using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Services;

public interface ISelfCheckoutService
{
    void InsertSelfCheckoutsFromCache(List<SelfCheckout> list);

    void TakeSelfCheckout(SelfCheckout selfCheckout);
}
