
namespace Profiles.Application.Commands.Receptionists.DeleteReceptionist
{
    public sealed record DeleteReceptionistCommand(int ReceptionistId) : IRequest<ErrorOr<Unit>>
    {
    }
}
