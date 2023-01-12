namespace Fedorakin.CashDesk.Web.Contracts.Responses;

public class CardResponse
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public double Total { get; set; }

    public double Discount { get; set; }

    public ProfileResponse Profile { get; set; } = null!;
}
