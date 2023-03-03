namespace Fedorakin.CashDesk.Web.Contracts.Requests.Product;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public double Price { get; set; }

    public double Weight { get; set; }

    public string Barcode { get; set; } = string.Empty;

    public string Photo { get; set; } = string.Empty;
}
