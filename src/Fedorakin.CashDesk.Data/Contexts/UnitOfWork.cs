using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces;

namespace Fedorakin.CashDesk.Data.Contexts;

public class UnitOfWork : IUnitOfWork
{
	private readonly DataContext _context;

	public UnitOfWork(DataContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
