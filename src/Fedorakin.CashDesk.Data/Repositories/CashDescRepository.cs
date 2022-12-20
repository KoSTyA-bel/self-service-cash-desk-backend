using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Data.Repositories;

public class CashDescRepository : ICashDescRepository
{
    private readonly DataContext _context;

    public CashDescRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task CreateCashDesc(CashDesc cashDesc, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCashDesc(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCashDesc(CashDesc cashDesc, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
