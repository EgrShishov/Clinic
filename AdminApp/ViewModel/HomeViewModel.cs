using AdminApp.Common.Abstractions;
using AdminApp.ViewModel.Appointment;
using AdminApp.ViewModel.Patient;
using CommunityToolkit.Mvvm.Input;

namespace AdminApp.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    public HomeViewModel()
    {
        // Startup Page
        ProfileName = "Shishov Egor Pavlovich";
    }

    [ObservableProperty]
    private object currentView;

    [ObservableProperty]
    private string profileName;

    [RelayCommand]
    private void ToServicesPage() => ChangeCurrentViewToServices();

    [RelayCommand]
    private void ToOfficesPage() => ChangeCurrentViewToOffices();

    [RelayCommand]
    private void ToAppointmentsPage() => ChangeCurrentViewToAppointments();

    [RelayCommand]
    private void ToReceptionistProfilesPage() => ChangeCurrentViewToReceptionistProfiles();

    [RelayCommand]
    private void ToDoctorsProfilePage() => ChangeCurrentViewToDoctorProfiles();

    [RelayCommand]
    private void ToPatientsProfilePage() => ChangeCurrentViewToPatientProfiles();


    private void ChangeCurrentViewToServices() => CurrentView = new ServicesViewModel();
    private void ChangeCurrentViewToOffices() => CurrentView = new OfficesViewModel();
    private void ChangeCurrentViewToAppointments() => CurrentView = new AppointmentViewModel();
    private void ChangeCurrentViewToDoctorProfiles() => CurrentView = new DoctorsScheduleViewModel();
    private void ChangeCurrentViewToReceptionistProfiles() => CurrentView = new ReceptionistProfileViewModel();
    private void ChangeCurrentViewToPatientProfiles() => CurrentView = new PatientProfileViewModel();
}
