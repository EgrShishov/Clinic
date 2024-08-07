public record DoctorProfileResponse(
    string FirstName,
    string LastName,
    string MiddleName,
    string PhotoUrl,
    DateTime DateOfBirth,
    int CareerStartYear,
    string SpecializationName,
    string City,
    string Street,
    string HouseNumber,
    string OfficeNumber);
