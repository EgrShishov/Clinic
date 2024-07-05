using InnoProfileslinic.Domain.Entities;

namespace Profiles.Application.Querires.Receptionists.ViewAllReceptionists
{
    public sealed record ViewAllReceptionistsQuery(int PageNumber, int PageSize) : IRequest<ErrorOr<List<Receptionist>>>
    {
    }
}
