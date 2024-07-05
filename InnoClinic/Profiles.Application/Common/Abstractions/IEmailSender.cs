
namespace Profiles.Application.Common.Abstractions
{
    public interface IEmailSender
    {
        Task SendEmailWithCredentialsAsync(string to, string credentials);
    }
}
