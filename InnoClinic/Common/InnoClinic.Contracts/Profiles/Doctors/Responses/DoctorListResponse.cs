public record DoctorListResponse(
    int Id,
    byte[] Photo,
    string LastName,
    string FirstName,
    string MiddleName,
    string Specialization,
    int Experience,
    string City,
    string Street,
    string HouseNumber,
    string OfficeNumber);