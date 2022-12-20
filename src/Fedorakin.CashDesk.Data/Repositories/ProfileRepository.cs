using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Data.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly DataContext _context;

    public ProfileRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task CreateProduct(Profile profile, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProfile(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProduct(Profile profile, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
