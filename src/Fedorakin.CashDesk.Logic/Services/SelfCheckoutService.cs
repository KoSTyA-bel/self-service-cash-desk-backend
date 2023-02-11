using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Services;

namespace Fedorakin.CashDesk.Logic.Services;

public class SelfCheckoutService : ISelfCheckoutService
{
    public void TakeSelfCheckout(SelfCheckout selfCheckout)
    {
        selfCheckout.IsBusy = true;
        selfCheckout.ActiveNumber = Guid.NewGuid();
    }
}
