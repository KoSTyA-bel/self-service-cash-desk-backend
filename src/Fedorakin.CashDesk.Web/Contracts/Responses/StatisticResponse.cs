using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Web.Contracts.Responses;

public class StatisticResponse
{
    public double TotalDiscount { get; set; }

    public double TotalAmount { get; set; }

    public double Total { get; set; }

    public double AveragePrice { get; set; }

    public IEnumerable<string> Products { get; set; }

    public IEnumerable<int> Counts { get; set; }
}
