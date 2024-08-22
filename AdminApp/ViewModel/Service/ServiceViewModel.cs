using CommunityToolkit.Mvvm.Input;

namespace AdminApp.ViewModel;
public partial class ServiceViewModel : ObservableObject
{
    public ServiceViewModel()
    {

    }

    [ObservableProperty]
    private ServiceModel service;

    [ObservableProperty]
    private bool isEditing;

    [ObservableProperty]
    private bool isNameValid;

    [ObservableProperty]
    private bool isPriceValid;

    [ObservableProperty]
    private bool isCategoryValid;

    [ObservableProperty]
    private string errorMessage;

    [RelayCommand]
    private void Edit() => throw new NotImplementedException();    
    
    [RelayCommand]
    private void Save() => throw new NotImplementedException();  
    
    [RelayCommand]
    private void Cancel() => throw new NotImplementedException();

    private void OnEditClicked()
    {
        isEditing = true;
    }

    private void OnCancelClicked()
    {
        isEditing = false;
    }

    private bool CanSave()
    {
        ValidateFields();
        return IsNameValid && IsPriceValid && IsCategoryValid;
    }

    private void OnSaveClicked()
    {
        isEditing = false;
    }

    private void ValidateFields()
    {
        IsNameValid = !string.IsNullOrWhiteSpace(Service.Name);
        IsPriceValid = Service.Price > 0;
        IsCategoryValid = !string.IsNullOrWhiteSpace(Service.CategoryName);
    }
}
