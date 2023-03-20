using Fedorakin.CashDesk.Data.Interfaces;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;

namespace Fedorakin.CashDesk.Logic.Managers;

public class MailManager : IMailManager
{
    private readonly IMailSender _mailSender;

    public MailManager(IMailSender mailSender)
    {
        _mailSender = mailSender ?? throw new ArgumentNullException(nameof(mailSender));
    }

    public Task SendCheck(string email, Check check)
    {
        return _mailSender.SendCheck(email, check);
    }
}
