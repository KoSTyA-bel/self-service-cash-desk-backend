using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Constants;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

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

    public async Task<Profile?> GetByIdAsync(int id)
    {
        var profiles = await GetRangeAsync(readIds: new ReadOnlyCollection<int>(new List<int> { id }));

        return profiles.SingleOrDefault();
    }

    public Task<List<Profile>> GetRangeAsync(
        int? page = default, 
        int? pageSize = default,
        IReadOnlyCollection<int>? readIds = default,
        params string[] includes)
    {
        var query = _context.Profiles.AsNoTracking();

        if (readIds is not null && readIds.Any())
        {
            query = query.Where(profile => readIds!.Contains(profile.Id));
        }

        if (includes.Contains(IncludeModels.ProfileNavigation.Role))
        {
            query = query.Include(profile => profile.Role);
        }

        if (page.HasValue && pageSize.HasValue)
        {
            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        return query.ToListAsync();
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
