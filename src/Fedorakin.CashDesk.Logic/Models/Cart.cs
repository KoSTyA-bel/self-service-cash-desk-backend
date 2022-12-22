namespace Fedorakin.CashDesk.Logic.Models;

public class Cart
{
    public int Id { get; set; }

    public int PoductId { get; set; }

    public Product? Product { get; set; }

    public Guid Number { get; set; }

    public DateTime Date { get; set; }

    public double Price { get; set; }
}
