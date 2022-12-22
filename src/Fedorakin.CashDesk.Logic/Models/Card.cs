namespace Fedorakin.CashDesk.Logic.Models;

public class Card
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public double Total { get; set; }

    public double Discount { get; set; }

    public int PersonId { get; set; }

    public Person? Person { get; set; }
}
