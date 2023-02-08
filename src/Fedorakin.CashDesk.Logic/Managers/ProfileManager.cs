using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Logic.Managers;

public class ProfileManager : IProfileManager
{
    private readonly DataContext _context;

    public ProfileManager(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task AddAsync(Profile model)
    {
        return _context.Profiles.AddAsync(model).AsTask();
    }

    public Task DeleteAsync(Profile model)
    {
        return Task.FromResult(_context.Profiles.Remove(model));
    }

    public Task<Profile?> GetByIdAsync(int id)
    {
        return _context.Profiles
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Profile>> GetRangeAsync(int page, int pageSize)
    {
        return _context.Profiles
            .AsNoTracking()
            .Include(x => x.Role)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task UpdateAsync(Profile model)
    {
        var profile = await GetByIdAsync(model.Id);

        if (profile is null)
        {
            return;
        }

        profile.FullName = model.FullName;
        profile.RoleId = model.RoleId;

        _context.Profiles.Update(profile);
    }
}
