public interface IEmailSender
{
    public Task SendEmailAsync(string userEmail, string subject, string body, Stream stream = null);
}
