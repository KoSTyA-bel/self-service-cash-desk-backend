using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Managers;

public interface IMailManager
{
    Task SendCheck(string email, Check check);
}
