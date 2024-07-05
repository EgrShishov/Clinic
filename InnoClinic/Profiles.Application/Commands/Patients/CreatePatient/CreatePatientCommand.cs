﻿using Microsoft.AspNetCore.Http;

namespace Profiles.Application.Commands.Patients.CreatePatient
{
    public sealed record CreatePatientCommand(
        int UserId,
        string FirstName,
        string LastName,
        string MiddleName,
        string PhoneNumber,
        DateTime DateOfBirth,
        IFormFile Photo) : IRequest<ErrorOr<Patient>>
    { }
}
