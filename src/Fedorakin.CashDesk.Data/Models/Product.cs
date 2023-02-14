namespace Fedorakin.CashDesk.Data.Models;

public class Product
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public double Price { get; set; }

    public double Weight { get; set; }

    public string Barcode { get; set; } = string.Empty;

    public string Photo { get; set; } = string.Empty;
}
