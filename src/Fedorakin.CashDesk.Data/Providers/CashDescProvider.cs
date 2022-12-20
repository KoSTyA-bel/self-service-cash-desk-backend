using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public class CashDescProvider : ICashDescProvider
{
    private readonly DbSet<CashDesc> _cashDescs;

    public CashDescProvider(DbSet<CashDesc> cashDescs)
    {
        _cashDescs = cashDescs ?? throw new ArgumentNullException(nameof(cashDescs));
    }

    public Task<CashDesc?> GetCashDesc(int id, CancellationToken cancellationToken)
    {
        return _cashDescs.FirstOrDefaultAsync(cashDesc => cashDesc.Id == id, cancellationToken);
    }

    public Task<IEnumerable<CashDesc>> GetRange(int page, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
