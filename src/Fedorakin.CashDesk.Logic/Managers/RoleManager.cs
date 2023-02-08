using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;

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

    public Task<Role?> GetByIdAsync(int id)
    {
        return _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Role>> GetRangeAsync(int page, int pageSize)
    {
        return _context.Roles
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
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
