namespace Fedorakin.CashDesk.Data.Settings;

public class MailSenderSettings
{
    public required string Name { get; set; }

    public required string Subject { get; set; }

    public required string Login { get; set; }

    public required string Password { get; set; }

    public required string SmtpServerUrl { get; set; }

    public int SmtpServerPort { get; set; }

    public bool SmtpUseSsl { get; set; }

    public required string MessageTemplate { get; set; }
}
