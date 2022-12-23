using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
	public RoleRepository(DbSet<Role> roles)
		: base(roles)
	{
	}
}
