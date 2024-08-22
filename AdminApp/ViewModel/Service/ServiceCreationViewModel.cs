using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AdminApp.ViewModel;
public partial class ServiceCreationViewModel : ObservableObject
{
    public ObservableCollection<SpecializationModel> Specializations { get; set; } = new();
    public ObservableCollection<ServiceCategory> Categories { get; set; } = new()
    {
        ServiceCategory.Analyses,
        ServiceCategory.Consultation,
        ServiceCategory.Diagnostics
    };

    public ServiceCreationViewModel()
    {

    }
    
    [ObservableProperty]
    private string servicePrice;

    [ObservableProperty]
    private string serviceName;

    [ObservableProperty]
    private ServiceCategory serviceCategory;

    [ObservableProperty]
    private bool isActive;

    [ObservableProperty]
    private bool isConfirmEnabled;

    [ObservableProperty]
    private SpecializationModel selectedSpecialization;

    [ObservableProperty]
    private string errorMessage;

    [RelayCommand]
    public async Task Confirm() => await CreateServiceAsync();

    [RelayCommand]
    public void Cancel() => CancelCreation();

    private async Task CreateServiceAsync()
    {
        IsConfirmEnabled = true;
        Validate();

        if ()
        {
            decimal.TryParse(ServicePrice, out decimal ServicePriceValue);

            var newService = new CreateServiceRequest
            {
                IsActive = IsActive,
                ServiceCategory = ServiceCategory,
                ServiceName = ServiceName,
                ServicePrice = ServicePriceValue,
                SpecializationId = SelectedSpecialization.Id
            };
        }

        throw new NotImplementedException();
    }

    private void CancelCreation()
    {
        throw new NotImplementedException();
    }

    private bool CanConfirm() => !string.IsNullOrEmpty(ServiceName) &&
                                !string.IsNullOrEmpty(ServicePrice) &&
                                decimal.TryParse(ServicePrice, out decimal priceValue) &&
                                priceValue > 0;
    private void Validate()
    {
        if (string.IsNullOrEmpty(ServiceName))
        {
            ErrorMessage = "Please, enter the name";
            IsConfirmEnabled = false;
        }
        else if (string.IsNullOrEmpty(ServicePrice) || !decimal.TryParse(ServicePrice, out decimal priceValue) || priceValue <= 0)
        {
            ErrorMessage = "You've entered an invalid price";
            IsConfirmEnabled = false;
        }
        else
        {
            ErrorMessage = string.Empty;
            IsConfirmEnabled = true;
        }
    }
}
