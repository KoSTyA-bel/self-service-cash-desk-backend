using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

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

    public Task<Cart?> GetByIdAsync(int id)
    {
        return _context.Carts
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Cart?> GetByNumberAsync(Guid number)
    {
        return _context.Carts
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Number == number);
    }

    public Task<List<Cart>> GetRangeAsync(int page, int pageSize)
    {
        return _context.Carts
            .AsNoTracking()
            .Include(x => x.Products)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
