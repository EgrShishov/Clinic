using AdminApp.Common.Abstractions;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AdminApp.ViewModel;

public partial class OfficesViewModel : BaseViewModel
{
    public ObservableCollection<OfficeModel> Offices { get; set; } = new();

    public OfficesViewModel()
    {
        Offices = new ObservableCollection<OfficeModel>
        {
            new OfficeModel { Id = "1", Location = "New York", ContactNumber = "123-456-7890", PhotoUrl = "Images/office1.png" },
            new OfficeModel { Id = "2", Location = "Los Angeles", ContactNumber = "987-654-3210", PhotoUrl = "Images/office2.png" },
            new OfficeModel { Id = "3", Location = "Chicago", ContactNumber = "456-789-0123", PhotoUrl = "Images/office3.png" },
        };
    }

    [RelayCommand]
    public void AddOffice() => OpenOfficeCreationModal();

    [RelayCommand]
    public void EditOffice() => OpenEditOfficeModal();

    [RelayCommand]
    public async Task DeleteOffice(OfficeModel office) => await DeleteOfficeOperation(office);

    [RelayCommand]
    public void ViewDetails(OfficeModel office) => throw new NotImplementedException();

    private void OpenOfficeCreationModal()
    {
        throw new NotImplementedException();
    }

    private void OpenEditOfficeModal()
    {
        throw new NotImplementedException();
    }
    private async Task DeleteOfficeOperation(OfficeModel office)
    {
        throw new NotImplementedException();
    }
}
