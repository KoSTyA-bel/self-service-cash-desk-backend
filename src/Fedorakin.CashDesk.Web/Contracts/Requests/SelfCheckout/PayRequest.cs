namespace Fedorakin.CashDesk.Web.Contracts.Requests.SelfCheckout;

public class PayRequest
{
    public int SelfCheckoutId { get; set; }

    public Guid CartNumber { get; set; }

    public string? CardCode { get; set; }
}
