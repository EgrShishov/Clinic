using System.Collections.ObjectModel;

namespace AdminApp.ViewModel;

public partial class ReceptionistCreationViewModel : ObservableObject
{
    public ObservableCollection<OfficeModel> offices { get; set; } = new();
    public ReceptionistCreationViewModel()
    {

    }

    [ObservableProperty]
    private string photoPath;

    [ObservableProperty]
    private string firstName;

    [ObservableProperty] 
    private string lastName;

    [ObservableProperty]
    private string middleName;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private OfficeModel selectedOffice;
}
