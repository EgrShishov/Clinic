public record CreateDoctorRequest(
    byte[] Photo,
    string FirstName,
    string LastName,
    string MiddleName,
    DateTime DateOfBirth,
    string Email,
    int SpecializationId,
    int OfficeId,
    DateTime CareerStartYear,
    string Status);
