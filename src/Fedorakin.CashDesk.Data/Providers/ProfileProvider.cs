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

    public override Task<Profile?> Get(int id, CancellationToken cancellationToken)
    {
        return _data
            .Where(x => x.Id == id)
            .Include(x => x.Role)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
