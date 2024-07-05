
namespace InnoClinic.Contracts.Authentication.Responses
{
    public sealed record AccountResponse(
          string PhoneNumber,
          int PhotoId,
          int CreatedBy,
          DateTime CreatedAt,
          int UpdatedBy,
          DateTime UpdatedAt
        );
}
