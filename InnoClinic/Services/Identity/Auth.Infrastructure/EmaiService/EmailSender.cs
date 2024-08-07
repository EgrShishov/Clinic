using MailKit.Net.Smtp;
using MimeKit;

public class EmailSender(EmailSettings emailSettings) : IEmailSender
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        SmtpClient smtp = new();
        smtp.Connect(emailSettings.Host, emailSettings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
        smtp.Authenticate(emailSettings.EmailAddress, emailSettings.Password);

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(emailSettings.EmailAddress));
        email.To.Add(new MailboxAddress("", to));
        email.Subject = subject;
        BodyBuilder messageBody = new BodyBuilder();    
        messageBody.HtmlBody = body;

        email.Body = messageBody.ToMessageBody();

        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}
