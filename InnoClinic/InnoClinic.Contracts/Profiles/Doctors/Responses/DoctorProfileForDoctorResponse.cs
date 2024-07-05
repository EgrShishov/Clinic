
namespace InnoClinic.Contracts.Profiles.Doctors.Responses
{
    public sealed record DoctorProfileForDoctorResponse(
       string FirstName,
       string LastName,
       string MiddleName,
       int PhotoId,
       DateTime DateOfBirth,
       DateTime CareetStartTime,
       int SpecializationId,
       string City,
       string Street,
       string HouseNumber,
       string OfficeNumber
       );
}
