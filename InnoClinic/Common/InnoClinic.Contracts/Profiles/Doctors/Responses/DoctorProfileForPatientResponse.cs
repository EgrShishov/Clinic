public sealed record DoctorProfileForPatientResponse(
    string FirstName,
    string LastName,
    string MiddleName,
    byte[] Photo,
    string Specialization,
    int Experience,
    string City,
    string Street,
    string HouseNumber,
    string OfficeNumber,
    List<SpecializationInfoResponse> specializations);
