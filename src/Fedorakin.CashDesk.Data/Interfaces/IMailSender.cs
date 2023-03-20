using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Data.Interfaces;

public interface IMailSender
{
    Task SendCheck(string email, Check check);
}
