namespace Fedorakin.CashDesk.Logic.Models;

public class Stock : BaseEntity
{
    public int Count { get; set; }

    public int ProductId { get; set; }

    public Product? Product { get; set; }
}
