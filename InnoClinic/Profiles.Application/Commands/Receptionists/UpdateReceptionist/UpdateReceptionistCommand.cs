using InnoProfileslinic.Domain.Entities;

namespace Profiles.Application.Commands.Receptionists.UpdateReceptionist
{
    public sealed record UpdateReceptionistCommand(
       int ReceptionistId,
       string FirstName,
       string LastName,
       string MiddleName,
       string Email,
       string OfficeId) : IRequest<ErrorOr<Receptionist>>
    {
    }
}
