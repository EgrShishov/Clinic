using AdminApp.Common.Abstractions;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AdminApp.ViewModel;

public partial class ServicesViewModel : BaseViewModel
{
    public ObservableCollection<ServiceModel> Services { get; set; } = new();
    public ServicesViewModel()
    {
        Services = new ObservableCollection<ServiceModel>
        {
            new ServiceModel { Id = 1, Name = "Teeth Whitening", Price = 199.99m, IsActive = true, CategoryName = "Dental" },
            new ServiceModel { Id = 2, Name = "General Check-up", Price = 50.00m, IsActive = true, CategoryName = "General Medicine" },
            new ServiceModel { Id = 3, Name = "Eye Test", Price = 75.00m, IsActive = false, CategoryName = "Optometry" }
        };
    }

    [RelayCommand]
    public void AddService() => OpenServiceCreationModal();

    [RelayCommand]
    public void EditService() => OpenEditServiceModal();

    [RelayCommand]
    public async Task DeleteService(ServiceModel service) => await DeleteServiceOperation(service);

    [RelayCommand]
    public void ViewDetails(ServiceModel service) => throw new NotImplementedException();

    private void OpenServiceCreationModal()
    {
        throw new NotImplementedException();
    }

    private void OpenEditServiceModal()
    {
        throw new NotImplementedException();
    }
    private async Task DeleteServiceOperation(ServiceModel office)
    {
        throw new NotImplementedException();
    }
}
