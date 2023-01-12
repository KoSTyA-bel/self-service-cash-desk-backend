using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public class ProfileProvider : BaseProvider<Profile>, IProfileProvider
{
    public ProfileProvider(DbSet<Profile> profiles)
        : base(profiles)
    {
    }

    protected override IQueryable<Profile> IncludeNavigationEntities(IQueryable<Profile> data)
    {
        return data
            .Include(x => x.Role);
    }
}
