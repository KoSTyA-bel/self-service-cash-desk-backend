using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly DbSet<Profile> _profiles;

    public ProfileRepository(DbSet<Profile> profiles)
    {
        _profiles = profiles ?? throw new ArgumentNullException(nameof(profiles));
    }

    public Task CreateProduct(Profile profile, CancellationToken cancellationToken)
    {
        return _profiles.AddAsync(profile, cancellationToken).AsTask();
    }

    public Task DeleteProfile(Profile profile, CancellationToken cancellationToken)
    {
        return Task.FromResult(_profiles.Remove(profile));
    }

    public Task UpdateProduct(Profile profile, CancellationToken cancellationToken)
    {
        return Task.FromResult(_profiles.Update(profile));
    }
}
