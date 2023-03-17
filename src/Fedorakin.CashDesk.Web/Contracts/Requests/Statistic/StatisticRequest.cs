namespace Fedorakin.CashDesk.Web.Contracts.Requests.Statistic;

public class StatisticRequest
{
    public required string Code { get; set; }

    public required string Cvv { get; set; }
}
