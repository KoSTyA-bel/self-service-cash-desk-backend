namespace Fedorakin.CashDesk.Web.Contracts.Responses;

public class SelfCheckoutResponse
{
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public bool IsBusy { get; set; }
}
