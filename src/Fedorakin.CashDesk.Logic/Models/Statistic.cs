using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Models;

public class Statistic
{
    public double TotalDiscount { get; set; }

    public double TotalAmount { get; set; }

    public double Total { get; set; }

    public double AveragePrice { get; set; }

    public Dictionary<string, int>? ProductsCount { get; set; }
}
