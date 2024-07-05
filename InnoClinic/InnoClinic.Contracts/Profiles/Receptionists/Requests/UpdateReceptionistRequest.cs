
namespace InnoClinic.Contracts.Profiles.Receptionists.Requests
{
    public sealed record UpdateReceptionistRequest(
       string FirstName,
       string LastName,
       string MiddleName,
       string Email,
       string OfficeId);
}
