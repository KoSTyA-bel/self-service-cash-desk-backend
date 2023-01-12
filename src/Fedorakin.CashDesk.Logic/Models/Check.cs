namespace Fedorakin.CashDesk.Logic.Models;

public class Check : BaseEntity
{
    public double Amount { get; set; }

    public double Discount { get; set; }

    public double Total { get; set; }

    public DateTime Date { get; set; }

    public Guid CartNumber { get; set; }

    public Card? Card { get; set; }

    public int SelfCheckoutId { get; set; }

    public SelfCheckout? SelfCheckout { get; set; }

}
