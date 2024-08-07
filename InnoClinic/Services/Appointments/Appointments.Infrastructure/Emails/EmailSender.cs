using MailKit.Net.Smtp;
using MimeKit;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _settings;
    public EmailSender(EmailSettings settings)
    {
        _settings = settings;
    }

    public async Task SendEmailAsync(string userEmail, string subject, string body, Stream stream = null)
    {
        var email = new MimeMessage();
       
        MailboxAddress emailFrom = new MailboxAddress(_settings.Name, _settings.EmailId);
        email.From.Add(emailFrom);

        MailboxAddress emailTo = new MailboxAddress("", userEmail);
        email.To.Add(emailTo);

        email.Subject = subject;
        BodyBuilder messageBody = new BodyBuilder();
        messageBody.HtmlBody = body;
        email.Body = messageBody.ToMessageBody();

        if (stream != null)
        {
            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(stream, ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName("")
            };
            messageBody.Attachments.Add(attachment);
        }

        SmtpClient smtp = new();
        smtp.Connect(_settings.Host, _settings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
        smtp.Authenticate(_settings.Email, _settings.Password);

        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}
