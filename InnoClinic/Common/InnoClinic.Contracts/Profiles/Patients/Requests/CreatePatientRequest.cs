public sealed record CreatePatientRequest(
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber,
    DateTime DateOfBirth,
    byte[] Photo);
