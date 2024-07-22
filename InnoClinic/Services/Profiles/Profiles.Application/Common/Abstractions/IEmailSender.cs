public interface IEmailSender
{
    Task SendEmailWithCredentialsAsync(string to, string credentials);
}
