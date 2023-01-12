namespace Fedorakin.CashDesk.Web.Contracts.Responses;

public class StockResponse
{
    public int Id { get; set; }

    public int Count { get; set; }

    public ProductResponse Product { get; set; } = null!;
}
