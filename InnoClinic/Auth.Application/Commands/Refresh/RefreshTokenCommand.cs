using InnoClinic.Contracts.Authentication.Responses;

namespace Auth.Application.Commands.Refresh
{
    public sealed record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<ErrorOr<RefreshTokenResponse>>
    {
    }
}
