using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Logic.Managers;

public class CheckManager : ICheckManager
{
    private readonly DataContext _context;

    public CheckManager(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task AddAsync(Check model)
    {
        return _context.Checks.AddAsync(model).AsTask();
    }

    public Task<Check?> GetByIdAsync(int id)
    {
        return _context.Checks
            .Include(x => x.Card)
            .ThenInclude(x => x.Profile)
            .ThenInclude(x => x.Role)
            .Include(x => x.SelfCheckout)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Check>> GetRangeAsync(int page, int pageSize)
    {
        return _context.Checks
            .AsNoTracking()
            .Include(x => x.Card)
            .ThenInclude(x => x.Profile)
            .ThenInclude(x => x.Role)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
