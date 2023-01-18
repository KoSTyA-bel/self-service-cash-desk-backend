namespace Fedorakin.CashDesk.Data.Models;

public class SelfCheckout
{
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public bool IsBusy { get; set; }

    public Guid ActiveNumber { get; set; }
}
