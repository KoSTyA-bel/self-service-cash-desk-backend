using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly DbSet<Person> _profiles;

    public ProfileRepository(DbSet<Person> profiles)
    {
        _profiles = profiles ?? throw new ArgumentNullException(nameof(profiles));
    }

    public Task CreateProduct(Person profile, CancellationToken cancellationToken)
    {
        return _profiles.AddAsync(profile, cancellationToken).AsTask();
    }

    public Task DeleteProfile(Person profile, CancellationToken cancellationToken)
    {
        return Task.FromResult(_profiles.Remove(profile));
    }

    public Task UpdateProduct(Person profile, CancellationToken cancellationToken)
    {
        return Task.FromResult(_profiles.Update(profile));
    }
}
