using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Constants;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Fedorakin.CashDesk.Logic.Managers;

public class RoleManager : IRoleManager
{
    private readonly DataContext _context;

    public RoleManager(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task AddAsync(Role model)
    {
        return _context.Roles.AddAsync(model).AsTask();
    }

    public Task DeleteAsync(Role model)
    {
        return Task.FromResult(_context.Roles.Remove(model));
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        var roles = await GetRangeAsync(readIds: new ReadOnlyCollection<int>(new List<int> { id }));

        return roles.SingleOrDefault();
    }

    public Task<List<Role>> GetRangeAsync(int? page = default, int? pageSize = default, IReadOnlyCollection<int>? readIds = default)
    {
        var query = _context.Roles.AsNoTracking();

        if (readIds is not null && readIds.Any())
        {
            query = query.Where(role => readIds!.Contains(role.Id));
        }

        if (page.HasValue && pageSize.HasValue)
        {
            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        return query.ToListAsync();
    }

    public async Task UpdateAsync(Role model)
    {
        var role = await GetByIdAsync(model.Id);

        if (role is null)
        {
            return;
        }

        role.Name = model.Name;

        _context.Roles.Update(role);
    }
}
