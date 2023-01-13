namespace Fedorakin.CashDesk.Web.Contracts.Responses;

public class CartResponse
{
    public Guid Number { get; set; }

    public DateTime Date { get; set; }

    public double Price { get; set; }

    public List<ProductResponse> Products { get; set; } = new();
}
