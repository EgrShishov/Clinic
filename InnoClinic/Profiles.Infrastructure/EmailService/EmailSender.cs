using Profiles.Application.Common.Abstractions;

namespace Profiles.Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailWithCredentialsAsync(string to, string credentials)
        {
            throw new NotImplementedException();
        }
    }
}
