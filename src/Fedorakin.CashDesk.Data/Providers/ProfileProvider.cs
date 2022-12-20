using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public class ProfileProvider : IProfileProvider
{
    private readonly DataContext _context;

    public ProfileProvider(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<Profile?> GetProdut(int id, CancellationToken cancellationToken)
    {
        return _context.Profiles.FirstOrDefaultAsync(profile => profile.Id == id);
    }

    public Task<IEnumerable<Profile>> GetRange(int page, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
