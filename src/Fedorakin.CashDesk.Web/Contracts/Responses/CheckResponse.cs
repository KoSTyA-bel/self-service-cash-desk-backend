namespace Fedorakin.CashDesk.Web.Contracts.Responses;

public class CheckResponse
{
    public int Id { get; set; }

    public double Amount { get; set; }

    public double Discount { get; set; }

    public double Total { get; set; }

    public DateTime Date { get; set; }

    public Guid CartNumber { get; set; }

    public CardResponse? Card { get; set; }

    public SelfCheckoutResponse SelfCheckout { get; set; }
}
