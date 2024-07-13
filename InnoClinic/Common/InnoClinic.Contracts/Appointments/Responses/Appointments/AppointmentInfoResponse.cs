public record AppointmentInfoResponse(
    TimeSpan AppointmentTime,
    string DoctorFullName,
    string PatientFullName,
    string PatientPhoneNumber,
    string ServiceName);
