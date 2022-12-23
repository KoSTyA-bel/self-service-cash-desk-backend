using Fedorakin.CashDesk.Logic.Interfaces;

namespace Fedorakin.CashDesk.Logic.Models;

public class Check : BaseEntity
{
    public int Id { get; set; }

    public double Amount { get; set; }

    public double Discount { get; set; }

    public double Total { get; set; }

    public DateTime Date { get; set; }

    public int CartId { get; set; }

    public Cart? Cart { get; set; }

    public int? CardId { get; set; }

    public Card? Card { get; set; }

    public int SelfCheckoutId { get; set; }

    public SelfCheckout? SelfCheckout { get; set; }

}
