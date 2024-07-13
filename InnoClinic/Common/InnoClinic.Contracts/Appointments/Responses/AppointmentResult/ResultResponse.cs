public sealed record ResultResponse(
    DateTime AppointmentDate,
    string PatientsFullName,
    DateTime PatientDateOfBirth,
    string DoctorsFullName,
    string DoctorsSpecialization,
    string ServiceName,
    string Complaints,
    string Conclusion,
    string Reccomendations);
