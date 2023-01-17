namespace Fedorakin.CashDesk.Logic.Models;

public class SelfCheckout : BaseEntity
{
    public bool IsActive { get; set; }

    public bool IsBusy { get; set; }

    public Guid ActiveNumber { get; set; }
}
