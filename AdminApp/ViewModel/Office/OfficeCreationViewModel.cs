using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Win32;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;

namespace AdminApp.ViewModel;

public partial class OfficeCreationViewModel : ObservableObject
{
    private readonly HttpClient _httpClient;

    public OfficeCreationViewModel()
    {

    }

    [ObservableProperty]
    private string city;

    [ObservableProperty]
    private string street;

    [ObservableProperty]
    private string houseNumber;

    [ObservableProperty]
    private string officeNumber;

    [ObservableProperty]
    private string phoneNumber;

    [ObservableProperty]
    private bool isActive;

    [ObservableProperty]
    private string photoPath;

    [ObservableProperty]
    private bool isConfirmEnabled;

    [RelayCommand]
    public async Task Confirm() => await CreateOfficeAsync();

    private void ValidateForm()
    {
        IsConfirmEnabled = !string.IsNullOrEmpty(City) &&
                          !string.IsNullOrEmpty(Street) &&
                          !string.IsNullOrEmpty(HouseNumber) &&
                          !string.IsNullOrEmpty(PhoneNumber) &&
                          PhoneNumber.StartsWith("+") &&
                          long.TryParse(PhoneNumber.TrimStart('+'), out _);
    }

    [RelayCommand]
    public void Cancel() => CancelCreation();

    [RelayCommand]
    public void BrowseOfficePhoto() => BrowsePhoto();
    private async Task CreateOfficeAsync()
    {
        IsConfirmEnabled = true;

        using var stream = new MemoryStream(File.ReadAllBytes(PhotoPath));
        
        var formFile = new FormFile(stream, 0, stream.Length, "", "");

        var newOffice = new CreateOfficeRequest
        {
            City = City,
            Street = Street,
            HouseNumber = HouseNumber,
            OfficeNumber = OfficeNumber,
            RegistryPhoneNumber = PhoneNumber,
            IsActive = IsActive,
            Photo = formFile
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("gateway/offices/create", newOffice);

            if (response.IsSuccessStatusCode)
            {
                // Navigate back to the office list page or show a success message
            }
            else
            {
                // Handle error
            }
        }
        catch (Exception)
        {                
            // Handle error
            throw;
        }
    }

    private void CancelCreation()
    {

    }

    private void BrowsePhoto()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
            Title = "Select an Office Photo"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            PhotoPath = openFileDialog.FileName;
        }
    }
}
