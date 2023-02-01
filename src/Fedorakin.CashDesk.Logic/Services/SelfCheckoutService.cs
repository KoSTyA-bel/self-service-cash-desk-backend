using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Services;

namespace Fedorakin.CashDesk.Logic.Services;

public class SelfCheckoutService : ISelfCheckoutService
{
    private readonly ICacheService _cacheService;

    public SelfCheckoutService(ICacheService cacheService)
    {
        _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
    }

    public void InsertSelfCheckoutsFromCache(List<SelfCheckout> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (_cacheService.TryGetSelfCheckout(list[i].Id, out SelfCheckout selfCheckout))
            {
                list[i] = selfCheckout;
            }
        }
    }

    public void TakeSelfCheckout(SelfCheckout selfCheckout)
    {
        selfCheckout.IsBusy = true;
        selfCheckout.ActiveNumber = Guid.NewGuid();
    }
}
