namespace Fedorakin.CashDesk.Web.Contracts.Requests.Cart;

public class RemoveProductFromCart
{
    public int SelfChecoutId { get; set; }

    public Guid CartNumber { get; set; }

    public int ProductId { get; set; }
}
