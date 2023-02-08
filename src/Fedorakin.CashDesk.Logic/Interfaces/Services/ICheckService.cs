using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Services;

public interface ICheckService
{
    void SetDateTime(Check check);
}
