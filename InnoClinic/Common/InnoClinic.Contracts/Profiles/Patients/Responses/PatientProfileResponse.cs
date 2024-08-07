public sealed record PatientProfileResponse(
    int UserId,
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber,
    DateTime DateOfBirth,
    string PhotoUrl);
