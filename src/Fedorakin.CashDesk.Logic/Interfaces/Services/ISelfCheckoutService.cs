using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Services;

public interface ISelfCheckoutService : IBaseService<SelfCheckout>
{
    public Task<Guid> TakeSelfCheckout(int id, CancellationToken cancellationToken);

    public Task Free(int id, CancellationToken cancellationToken);
}
