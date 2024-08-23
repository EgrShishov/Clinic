using AdminApp.Common.Abstractions;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AdminApp.ViewModel;

public partial class DoctorsScheduleViewModel : BaseViewModel
{
    public ObservableCollection<AppointmentListModal> Appointments { get; set; } = new();
    public DoctorsScheduleViewModel()
    {
        LoadAppointments();
        selectedDate = DateTime.UtcNow.Date;
    }

    [ObservableProperty]
    private DateTime selectedDate;

    [RelayCommand]
    public void ViewProfile(AppointmentListModal appointment) => ViewPatientProfile(appointment);

    [RelayCommand]
    public void AddResults(AppointmentListModal appointment) => ShowAddResultsModal(appointment); 

    [RelayCommand]
    public void LoadAppointments()
    {
        // httpclient call mb
        Appointments.Add(new AppointmentListModal { Time = "09:00 - 09:20", PatientName = "John Doe", ServiceName = "Initial Consultation", IsApproved = true });
        Appointments.Add(new AppointmentListModal { Time = "09:30 - 09:50", PatientName = "Jane Smith", ServiceName = "Follow-up", IsApproved = false });
        Appointments.Add(new AppointmentListModal { Time = "09:30 - 09:50", PatientName = "Jane Smith", ServiceName = "Follow-up", IsApproved = false });
        Appointments.Add(new AppointmentListModal { Time = "09:30 - 09:50", PatientName = "Jane Smith", ServiceName = "Follow-up", IsApproved = false });
        Appointments.Add(new AppointmentListModal { Time = "09:30 - 09:50", PatientName = "Jane Smith", ServiceName = "Follow-up", IsApproved = false });
        Appointments.Add(new AppointmentListModal { Time = "09:30 - 09:50", PatientName = "Jane Smith", ServiceName = "Follow-up", IsApproved = false });
        Appointments.Add(new AppointmentListModal { Time = "09:30 - 09:50", PatientName = "Jane Smith", ServiceName = "Follow-up", IsApproved = false });
        Appointments.Add(new AppointmentListModal { Time = "09:30 - 09:50", PatientName = "Jane Smith", ServiceName = "Follow-up", IsApproved = false });
        Appointments.Add(new AppointmentListModal { Time = "09:30 - 09:50", PatientName = "Jane Smith", ServiceName = "Follow-up", IsApproved = false });
        Appointments.Add(new AppointmentListModal { Time = "09:30 - 09:50", PatientName = "Jane Smith", ServiceName = "Follow-up", IsApproved = false });
        Appointments.Add(new AppointmentListModal { Time = "09:30 - 09:50", PatientName = "Jane Smith", ServiceName = "Follow-up", IsApproved = false });
    }

    private void ViewPatientProfile(AppointmentListModal appointment)
    {
        // Logic to view profile
    }

    private void ShowAddResultsModal(AppointmentListModal appointment)
    {
        // Logic to add medical results
    }
}
