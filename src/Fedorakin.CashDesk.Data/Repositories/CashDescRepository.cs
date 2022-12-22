using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Repositories;

public class CashDescRepository : ICashDescRepository
{
    private readonly DbSet<SelfCheckout> _cashDescs;

    public CashDescRepository(DbSet<SelfCheckout> cashDescs)
    {
        _cashDescs = cashDescs ?? throw new ArgumentNullException(nameof(cashDescs));
    }

    public Task CreateCashDesc(SelfCheckout cashDesc, CancellationToken cancellationToken)
    {
        return _cashDescs.AddAsync(cashDesc, cancellationToken).AsTask();
    }

    public Task DeleteCashDesc(SelfCheckout cashDesc, CancellationToken cancellationToken)
    {
        return Task.FromResult(_cashDescs.Remove(cashDesc));
    }

    public Task UpdateCashDesc(SelfCheckout cashDesc, CancellationToken cancellationToken)
    {
        return Task.FromResult(_cashDescs.Update(cashDesc));
    }
}
