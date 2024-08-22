using CommunityToolkit.Mvvm.Input;

namespace AdminApp.ViewModel;

public partial class DoctorProfileViewModel : ObservableObject
{
    public DoctorProfileViewModel()
    {
        
    }

    [ObservableProperty]
    private DoctorModel doctorsProfile;

    [RelayCommand]
    private async Task LoadDoctorsScheduleCommand(int doctorId)
    {
        //var schedule = await 
        throw new NotImplementedException();
    }
}