using InnoClinic.Contracts.Authentication.Responses;

namespace Auth.Application.Commands.SignIn
{
    public sealed record SignInCommand(string Email, string Password, string Role) 
        : IRequest<ErrorOr<AuthorizationResponse>> { }
}
