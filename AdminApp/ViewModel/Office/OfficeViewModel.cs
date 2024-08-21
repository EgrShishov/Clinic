using CommunityToolkit.Mvvm.Input;
using System.Net.Http;

namespace AdminApp.ViewModel;

public partial class OfficeViewModel : ObservableObject
{
    private readonly HttpClient _httpClient;

    public OfficeViewModel()
    {

    }

    [ObservableProperty]
    private List<OfficeModel> offices;

    [ObservableProperty]
    private bool isViewMode;

    [ObservableProperty]
    private bool isEditing;

    [RelayCommand]
    private async Task Edit() => IsEditing = true;

    [RelayCommand]
    private async Task Save() => SaveOffice();

    [RelayCommand]
    private async Task Cancel() => CancelAction();

    private void SaveOffice()
    {
        // some logic
        IsEditing = false;
    }

    private void CancelAction()
    {
        // Cancel logic here
        IsEditing = false;
    }
}
