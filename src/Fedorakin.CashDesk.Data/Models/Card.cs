namespace Fedorakin.CashDesk.Data.Models;

public class Card
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public double Total { get; set; }

    public double Discount { get; set; }

    public int ProfileId { get; set; }

    public Profile? Profile { get; set; }
}
