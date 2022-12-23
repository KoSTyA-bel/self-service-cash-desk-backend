using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public class RoleProvider : BaseProvider<Role>, IRoleProvider
{
	public RoleProvider(DbSet<Role> roles)
		: base(roles)
	{
	}
}
