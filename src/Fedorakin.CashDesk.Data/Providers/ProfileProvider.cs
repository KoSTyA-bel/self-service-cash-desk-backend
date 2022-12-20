using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public class ProfileProvider : IProfileProvider
{
    private readonly DbSet<Profile> _profiles;

    public ProfileProvider(DbSet<Profile> profiles)
    {
        _profiles = profiles ?? throw new ArgumentNullException(nameof(profiles));
    }

    public Task<Profile?> GetProfile(int id, CancellationToken cancellationToken)
    {
        return _profiles.FirstOrDefaultAsync(profile => profile.Id == id, cancellationToken);
    }

    public Task<IEnumerable<Profile>> GetRange(int page, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
