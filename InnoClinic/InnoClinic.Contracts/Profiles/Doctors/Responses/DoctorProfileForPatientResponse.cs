
namespace InnoClinic.Contracts.Profiles.Doctors.Responses
{
    public sealed record DoctorProfileForPatientResponse(
        string FirstName,
        string LastName,
        string MiddleName,
        int PhotoId,
        int SpecializationId,
        int Experience,
        string City,
        string Street,
        string HouseNumber,
        string OfficeNumber
        );
}
