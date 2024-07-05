using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Commands.VerifyEmail
{
    public record VerifyEmailCommand(string Link) : IRequest<ErrorOr<Unit>> 
    {
    }
}   
