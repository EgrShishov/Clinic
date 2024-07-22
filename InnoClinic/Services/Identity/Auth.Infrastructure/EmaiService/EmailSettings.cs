public class EmailSettings
{
    public const string SectionName = "EmailSettings";
    public string Host { get; set; }
    public string EmailAddress { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }

}       