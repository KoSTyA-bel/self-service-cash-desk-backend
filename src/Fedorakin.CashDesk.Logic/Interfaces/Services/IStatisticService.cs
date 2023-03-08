using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Services;

public interface IStatisticService

{
    Statistic CalculateStatistic(IEnumerable<Check> checks, IEnumerable<Cart> carts);
}
