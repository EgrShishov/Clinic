public sealed record DoctorProfileForDoctorResponse(
    byte[] Photo,
    string FirstName,
    string LastName,
    string MiddleName,
    DateTime DateOfBirth,
    DateTime CareetStartTime,
    int SpecializationId,
    string City,
    string Street,
    string HouseNumber,
    string OfficeNumber);