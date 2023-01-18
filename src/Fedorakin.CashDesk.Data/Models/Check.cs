namespace Fedorakin.CashDesk.Data.Models;

public class Check
{
    public int Id { get; set; }

    public double Amount { get; set; }

    public double Discount { get; set; }

    public double Total { get; set; }

    public DateTime Date { get; set; }

    public Guid CartNumber { get; set; }

    public Card? Card { get; set; }

    public int SelfCheckoutId { get; set; }

    public SelfCheckout? SelfCheckout { get; set; }

}
