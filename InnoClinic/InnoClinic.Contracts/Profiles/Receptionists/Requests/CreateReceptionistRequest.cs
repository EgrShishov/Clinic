
using Microsoft.AspNetCore.Http;

namespace InnoClinic.Contracts.Profiles.Receptionists.Requests
{
    public sealed record CreateReceptionistRequest(
       IFormFile Photo,
       string FirstName,
       string LastName,
       string MiddleName,
       string Email,
       string OfficeId);
}