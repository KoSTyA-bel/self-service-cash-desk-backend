using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Repositories;

public interface ICashDescRepository
{
    public Task CreateCashDesc(CashDesc cashDesc, CancellationToken cancellationToken);

    public Task UpdateCashDesc(CashDesc cashDesc, CancellationToken cancellationToken);

    public Task DeleteCashDesc(CashDesc cashDesc, CancellationToken cancellationToken);
}
