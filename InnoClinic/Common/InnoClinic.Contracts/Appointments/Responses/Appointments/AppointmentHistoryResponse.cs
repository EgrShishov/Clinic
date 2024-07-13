public record AppointmentHistoryResponse(
    DateTime AppointmentDate,
    TimeSpan AppointmentTime,
    string DoctorFullName,
    string ServiceName,
    string LinkToMedicalResults);