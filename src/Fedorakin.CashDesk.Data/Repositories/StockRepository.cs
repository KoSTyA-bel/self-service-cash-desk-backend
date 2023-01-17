using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Repositories;

public class StockRepository : BaseRepository<Stock>, IStockRepository
{
	public StockRepository(DbSet<Stock> stocks)
		: base(stocks)
	{
	}
}
