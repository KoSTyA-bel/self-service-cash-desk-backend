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

    public Task<Stock?> GetStockForProduct(int productId, CancellationToken cancellationToken)
    {
        return IncludeNavigationEntities(_data)
            .FirstOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
    }

    protected override IQueryable<Stock> IncludeNavigationEntities(IQueryable<Stock> data)
    {
        return data
            .Include(x => x.Product);
    }
}
