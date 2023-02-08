namespace Fedorakin.CashDesk.Web.Contracts.Requests.SelfCheckout;

public class MakeSelfCheckoutFreeRequest
{
    public int Id { get; set; }

    public Guid CartNumber { get; set; }
}
