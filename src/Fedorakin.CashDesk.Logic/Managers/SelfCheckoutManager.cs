using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

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

    public async Task<SelfCheckout?> GetByIdAsync(int id)
    {
        var selfCheckouts = await GetRangeAsync(readIds: new ReadOnlyCollection<int>(new List<int> { id}));

        return selfCheckouts.SingleOrDefault();
    }

    public Task<List<SelfCheckout>> GetRangeAsync(int? page = default, int? pageSize = default, IReadOnlyCollection<int>? readIds = default)
    {
        var query = _context.SelfCheckouts.AsNoTracking();

        if (readIds is not null && readIds.Any())
        {
            query = query.Where(selfCheckout => readIds!.Contains(selfCheckout.Id));
        }

        if (page.HasValue && pageSize.HasValue)
        {
            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        return query.ToListAsync();
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
