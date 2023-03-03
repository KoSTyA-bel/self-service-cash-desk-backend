namespace Fedorakin.CashDesk.Web.Contracts.Requests.Card;

public class CreateCardRequest
{
    public string Code { get; set; } = string.Empty;

    public string CVV { get; set; } = string.Empty;

    public double Discount { get; set; }

    public int ProfileId { get; set; }
}
