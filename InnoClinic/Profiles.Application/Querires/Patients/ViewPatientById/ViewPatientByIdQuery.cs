public sealed record ViewPatientByIdQuery(int PatientId) : IRequest<ErrorOr<Patient>>
{
}
