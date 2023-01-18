namespace Fedorakin.CashDesk.Data.Models;

public class Cart
{
    public int Id { get; set; }

    public List<Product> Products { get; set; } = new();

    public Guid Number { get; set; }

    public DateTime Date { get; set; }

    public double Price { get; set; }
}
