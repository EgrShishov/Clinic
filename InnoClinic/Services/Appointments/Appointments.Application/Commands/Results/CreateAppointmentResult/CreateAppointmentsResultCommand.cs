﻿public sealed record CreateAppointmentsResultCommand(
    DateTime? AppointmentDate,
    int PatientId,
    DateTime? DateofBirth,
    int DoctorId,
    int SpecializationId,
    int ServiceId,
    string Complaints,
    string Conclusion,
    string Recommendations) : IRequest<ErrorOr<Results>>
{
}