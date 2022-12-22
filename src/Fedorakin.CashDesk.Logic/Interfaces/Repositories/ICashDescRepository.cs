using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Repositories;

public interface ICashDescRepository
{
    public Task CreateCashDesc(SelfCheckout cashDesc, CancellationToken cancellationToken);

    public Task UpdateCashDesc(SelfCheckout cashDesc, CancellationToken cancellationToken);

    public Task DeleteCashDesc(SelfCheckout cashDesc, CancellationToken cancellationToken);
}
