using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Fedorakin.CashDesk.Logic.Services;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly ITimeSpanProvider _timeSpanProvider;

    public CacheService(IMemoryCache cache, ITimeSpanProvider timeSpanProvider)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _timeSpanProvider = timeSpanProvider ?? throw new ArgumentNullException(nameof(timeSpanProvider));
    }

    public void RemoveCart(Guid number)
    {
        _cache.Remove(number);
    }

    public void RemoveSelfCheckout(int id)
    {
        _cache.Remove(id);
    }

    public void SetCart(Cart cart)
    {
        _cache.Set(cart.Number, cart, new MemoryCacheEntryOptions().SetAbsoluteExpiration(_timeSpanProvider.ForFiveMinutes()));
    }

    public void SetSelfCheckout(SelfCheckout selfCheckout)
    {
        _cache.Set(selfCheckout.Id, selfCheckout, new MemoryCacheEntryOptions().SetAbsoluteExpiration(_timeSpanProvider.ForFiveMinutes()));
    }

    public bool TryGetCart(Guid number, out Cart? cart)
    {        
        return _cache.TryGetValue(number, out cart);
    }

    public bool TryGetSelfCheckout(int id, out SelfCheckout? selfCheckout)
    {
        return _cache.TryGetValue(id, out selfCheckout);
    }
}
