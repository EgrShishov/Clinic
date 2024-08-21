using CommunityToolkit.Mvvm.Input;

namespace AdminApp.ViewModel
{
    public partial class NavigationViewModel : ObservableObject
    {
        public NavigationViewModel()
        {
            // Startup Page
            CurrentView = new SignInViewModel();
        }

        [ObservableProperty]
        private object currentView;

        [RelayCommand]
        private void ToSignInPage() => ChangeCurrentViewToSignIn();

        [RelayCommand]
        private void ToHomePage() => ChangeCurrentViewToHome();

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


        private void ChangeCurrentViewToSignIn() => CurrentView = new SignInViewModel();
        private void ChangeCurrentViewToHome() => CurrentView = new HomeViewModel();
        private void ChangeCurrentViewToServices() => CurrentView = new ServiceViewModel();
        private void ChangeCurrentViewToOffices() => throw new NotImplementedException();//CurrentView = new OfficeViewModel();
        private void ChangeCurrentViewToAppointments() => CurrentView = new AppointmentViewModel();
        private void ChangeCurrentViewToDoctorProfiles() => CurrentView = new DoctorProfileViewModel();
        private void ChangeCurrentViewToReceptionistProfiles() => CurrentView = new ReceptionistProfileViewModel();
        private void ChangeCurrentViewToPatientProfiles() => CurrentView = new PatientProfileViewModel();
    }
}
