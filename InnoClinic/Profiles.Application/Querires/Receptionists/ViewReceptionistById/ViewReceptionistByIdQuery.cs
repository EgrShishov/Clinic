using InnoProfileslinic.Domain.Entities;

namespace Profiles.Application.Querires.Receptionists.ViewReceptionistById
{
    public sealed record ViewReceptionistByIdQuery(int ReceptionistId) : IRequest<ErrorOr<Receptionist>>
    {
    }
}
