using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public class CheckProvider: BaseProvider<Check>, ICheckProvider
{
	public CheckProvider(DbSet<Check> checks)
		: base(checks)
	{
	}

    public override Task<Check?> Get(int id, CancellationToken cancellationToken)
    {
        return _data
            .Where(x => x.Id == id)
            .Include(x => x.Card)
            .Include(x => x.SelfCheckout)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
