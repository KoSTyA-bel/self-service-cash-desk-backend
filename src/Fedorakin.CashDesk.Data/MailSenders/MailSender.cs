using Fedorakin.CashDesk.Data.Interfaces;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Data.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Fedorakin.CashDesk.Data.MailSenders;

public class MailSender : IMailSender
{
    private readonly MailSenderSettings _settings;

    public MailSender(MailSenderSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    public async Task SendCheck(string email, Check check)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(MailboxAddress.Parse(_settings.Login));
        emailMessage.To.Add(MailboxAddress.Parse(email));
        emailMessage.Subject = _settings.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = string.Format(_settings.MessageTemplate,
                                    check.Id,
                                    check.Date,
                                    check.Amount,
                                    check.Discount,
                                    check.Total,
                                    check.CartNumber,
                                    check.Card is null ? "-" : check.Card.Code)
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(_settings.SmtpServerUrl, _settings.SmtpServerPort, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_settings.Login, _settings.Password);
        await client.SendAsync(emailMessage);

        await client.DisconnectAsync(true);

    }
}
