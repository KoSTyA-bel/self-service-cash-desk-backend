using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Repositories;

public class CheckRepository : BaseRepository<Check>, ICheckRepository
{
	public CheckRepository(DbSet<Check> checks)
		: base(checks)
	{
	}
}
