namespace Fedorakin.CashDesk.Web.Contracts.Requests.Check;

public class SendCheckRequest
{
    public required string Email { get; set; }

    public int CheckId { get; set; }
}
