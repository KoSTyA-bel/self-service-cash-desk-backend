using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Services;

public interface ISelfCheckoutService
{
    void TakeSelfCheckout(SelfCheckout selfCheckout);
}
