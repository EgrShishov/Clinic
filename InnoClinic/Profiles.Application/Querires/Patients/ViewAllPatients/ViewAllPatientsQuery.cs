﻿
namespace Profiles.Application.Querires.Patients.ViewAllPatients
{
    public sealed record ViewAllPatientsQuery(int PageSize, int PageNumber) : IRequest<ErrorOr<List<Patient>>>
    {
    }
}
