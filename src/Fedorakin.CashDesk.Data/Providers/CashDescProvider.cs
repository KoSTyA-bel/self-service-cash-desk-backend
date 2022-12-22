using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public class CashDescProvider : ICashDescProvider
{
    private readonly DbSet<SelfCheckout> _cashDescs;

    public CashDescProvider(DbSet<SelfCheckout> cashDescs)
    {
        _cashDescs = cashDescs ?? throw new ArgumentNullException(nameof(cashDescs));
    }

    public Task<SelfCheckout?> GetCashDesc(int id, CancellationToken cancellationToken)
    {
        return _cashDescs.FirstOrDefaultAsync(cashDesc => cashDesc.Id == id, cancellationToken);
    }

    public Task<IEnumerable<SelfCheckout>> GetRange(int page, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
