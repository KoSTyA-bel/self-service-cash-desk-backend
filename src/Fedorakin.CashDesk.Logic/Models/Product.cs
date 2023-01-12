namespace Fedorakin.CashDesk.Logic.Models;

public class Product : BaseEntity 
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public double Price { get; set; }

    public double Weight { get; set; }

    public string Barcode { get; set; } = string.Empty;
}
