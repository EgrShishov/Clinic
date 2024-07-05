using Officies.Domain.Common.Errors;

namespace Officies.Application.Commands.ChangeOfficesStatus
{
    public record ChangeOfficesStatusCommand(string Id, bool isActive) : IRequest<ErrorOr<Unit>>
    {
    }
}
