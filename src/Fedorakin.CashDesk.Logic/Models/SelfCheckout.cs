using Fedorakin.CashDesk.Logic.Interfaces;

namespace Fedorakin.CashDesk.Logic.Models;

public class SelfCheckout : BaseEntity
{
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public bool IsBusy { get; set; }

    public Guid ActiveNumber { get; set; }
}
