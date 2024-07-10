public sealed record CreateAppointmentsResultCommand(
    DateTime Date,
    string PatientFullName,
    DateTime DateofBirth,
    string DoctorFullName,
    string Specialization,
    string ServiceName,
    string Complaints,
    string Conclusion,
    string Recommendations) : IRequest<ErrorOr<Results>>
{
}
