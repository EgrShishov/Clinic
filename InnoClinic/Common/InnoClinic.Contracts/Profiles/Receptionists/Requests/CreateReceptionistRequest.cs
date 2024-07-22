public record CreateReceptionistRequest(
    byte[] Photo,
    string FirstName,
    string LastName,
    string MiddleName,
    string Email,
    string OfficeId);