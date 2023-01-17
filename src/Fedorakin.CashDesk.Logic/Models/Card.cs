namespace Fedorakin.CashDesk.Logic.Models;

public class Card : BaseEntity
{
    public string Code { get; set; } = string.Empty;

    public double Total { get; set; }

    public double Discount { get; set; }

    public int ProfileId { get; set; }

    public Profile? Profile { get; set; }
}
