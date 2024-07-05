
namespace InnoClinic.Contracts.Profiles.Receptionists.Responses
{
    public sealed record ReceptionistResponse(
           int id,
           string FirstName,
           string LastName,
           string MiddleName,
           string Email,
           string City,
           string Street,
           string HouseNumber,
           string OfficeNumber);
}
