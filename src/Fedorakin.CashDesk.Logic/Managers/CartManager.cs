using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Constants;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Fedorakin.CashDesk.Logic.Managers;

public class CartManager : ICartManager
{
    private readonly DataContext _context;

    public CartManager(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task AddAsync(Cart model)
    {
        return _context.Carts.AddAsync(model).AsTask();
    }

    public async Task<Cart?> GetByIdAsync(int id)
    {
        var cards = await GetRangeAsync(readOnlyIds: new ReadOnlyCollection<int>(new List<int> { id }));

        return cards.SingleOrDefault();
    }

    public async Task<Cart?> GetByNumberAsync(Guid number)
    {
        var cards = await GetRangeAsync(readOnlyNumbers: new ReadOnlyCollection<Guid>(new List<Guid> { number }));

        return cards.SingleOrDefault();
    }

    public Task<List<Cart>> GetRangeAsync(
        int? page = default,
        int? pageSize = default,
        IReadOnlyCollection<int>? readOnlyIds = default,
        IReadOnlyCollection<Guid>? readOnlyNumbers = default,
        params string[] includes)
    {
        var query = _context.Carts.AsNoTracking();

        if (readOnlyIds is not null && readOnlyIds.Any())
        {
            query = query.Where(cart => readOnlyIds!.Contains(cart.Id));
        }

        if (readOnlyNumbers is not null && readOnlyNumbers.Any())
        {
            query = query.Where(cart => readOnlyNumbers!.Contains(cart.Number));
        }

        if (includes.Contains(IncludeModels.CartNavigation.Products))
        {
            query = query.Include(cart => cart.Products);
        }

        if (page.HasValue && pageSize.HasValue)
        {
            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        return query.ToListAsync();
    }
}
