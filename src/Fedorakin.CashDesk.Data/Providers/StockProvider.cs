using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public class StockProvider : BaseProvider<Stock>, IStockProvider
{
	public StockProvider(DbSet<Stock> stocks)
		: base(stocks)
	{
	}

    public override Task<Stock?> Get(int id, CancellationToken cancellationToken)
    {
        return _data
            .Where(x => x.Id == id)
            .Include(x => x.Product)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
