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
}
