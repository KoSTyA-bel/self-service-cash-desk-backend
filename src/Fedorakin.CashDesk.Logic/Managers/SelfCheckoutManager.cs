using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Logic.Managers;

public class SelfCheckoutManager : ISelfCheckoutManager
{
    private readonly DataContext _context;

    public SelfCheckoutManager(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task AddAsync(SelfCheckout model)
    {
        return _context.SelfCheckouts.AddAsync(model).AsTask();
    }

    public Task DeleteAsync(SelfCheckout model)
    {
        return Task.FromResult(_context.SelfCheckouts.Remove(model));
    }

    public Task<SelfCheckout?> GetByIdAsync(int id)
    {
        return _context.SelfCheckouts.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<SelfCheckout>> GetRangeAsync(int page, int pageSize)
    {
        return _context.SelfCheckouts
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task UpdateAsync(SelfCheckout model)
    {
        var selfCheckout = await GetByIdAsync(model.Id);

        if (selfCheckout is null)
        {
            return;
        }

        selfCheckout.IsActive = model.IsActive;

        _context.SelfCheckouts.Update(selfCheckout);
    }
}
