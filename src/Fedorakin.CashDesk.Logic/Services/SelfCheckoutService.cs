using Fedorakin.CashDesk.Logic.Exceptions;
using Fedorakin.CashDesk.Logic.Interfaces;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Fedorakin.CashDesk.Logic.Services;

public class SelfCheckoutService : ServiceBase<SelfCheckout>, ISelfCheckoutService
{
    private readonly IMemoryCache _cache;
    private readonly ITimeSpanProvider _timeSpamProvider;

    public SelfCheckoutService(ISelfCheckoutProvider provider,
        ISelfCheckoutRepository repository,
        IUnitOfWork unitOfWork,
        ITimeSpanProvider timeSpanProvider,
        IMemoryCache memoryCache) 
        : base(provider, repository, unitOfWork)
    {
        _timeSpamProvider= timeSpanProvider ?? throw new ArgumentNullException(nameof(timeSpanProvider));
        _cache= memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public Task Free(int id, CancellationToken cancellationToken)
    {
        _cache.Remove(id);

        return Task.CompletedTask;
    }

    public override Task<SelfCheckout> Get(int id, CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(id, out object obj))
        {
            return Task.FromResult(obj as SelfCheckout);
        }

        return base.Get(id, cancellationToken);
    }

    public override async Task<List<SelfCheckout>> GetRange(int page, int pageSize, CancellationToken cancellationToken)
    {
        var selfChechouts = await base.GetRange(page, pageSize, cancellationToken);

        for (int i = 0; i < selfChechouts.Count; i++)
        {
            if (_cache.TryGetValue(selfChechouts[i].Id, out object obj))
            {
                selfChechouts[i] = (SelfCheckout)obj;
            }
        }

        return selfChechouts;
    }

    public async Task<Guid> TakeSelfCheckout(int id, CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(id, out _))
        {
            throw new SelfCheckoutBusyException();
        }

        var selfCheckout = await Get(id, cancellationToken);

        if (selfCheckout is null)
        {
            throw new ArgumentNullException(nameof(selfCheckout));
        }

        if (!selfCheckout.IsActive)
        {
            throw new SelfCheckoutUnactiveException();
        }

        selfCheckout.IsBusy = true;
        selfCheckout.ActiveNumber = Guid.NewGuid();

        _cache.Set(selfCheckout.Id, selfCheckout, new MemoryCacheEntryOptions().SetAbsoluteExpiration(_timeSpamProvider.ForFiveMinutes()));

        return selfCheckout.ActiveNumber;
    }
}
