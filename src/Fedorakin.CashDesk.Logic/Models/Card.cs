using Fedorakin.CashDesk.Logic.Interfaces;

namespace Fedorakin.CashDesk.Logic.Models;

public class Card : BaseEntity
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public double Total { get; set; }

    public double Discount { get; set; }

    public int ProfileId { get; set; }

    public Profile? Person { get; set; }
}
