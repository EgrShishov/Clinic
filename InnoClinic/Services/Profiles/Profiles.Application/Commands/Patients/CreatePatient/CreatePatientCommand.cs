using Microsoft.AspNetCore.Http;

public sealed record CreatePatientCommand(
    int AccountId,
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber,
    DateTime DateOfBirth,
    IFormFile Photo) : IRequest<ErrorOr<CreatePatientResponse>>
{ }
