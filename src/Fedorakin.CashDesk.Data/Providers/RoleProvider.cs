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

    public override Task<Role?> Get(int id, CancellationToken cancellationToken)
    {
       return _data.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
