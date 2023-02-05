using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Logic.Managers;

public class StockManager : IStockManager
{
    private readonly DataContext _context;

    public StockManager(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task AddAsync(Stock model)
    {
        return _context.Stocks.AddAsync(model).AsTask();
    }

    public Task DeleteAsync(Stock model)
    {
        return Task.FromResult(_context.Stocks.Remove(model));
    }

    public Task<Stock?> GetByIdAsync(int id)
    {
        return _context.Stocks
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Stock>> GetRangeAsync(int page, int pageSize, string name, string barcode)
    {
        return _context.Stocks
            .AsNoTracking()
            .Include(x => x.Product)
            .Where(x => x.Product.Name.Contains(name) && x.Product.Barcode.Contains(barcode))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public Task<Stock?> GetStockForProductAsync(int productId)
    {
        return _context.Stocks
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.ProductId == productId);
    }

    public async Task UpdateAsync(Stock model)
    {
        var stock = await GetByIdAsync(model.Id);

        if (stock is null)
        {
            return;
        }

        stock.ProductId = model.ProductId;
        stock.Count = model.Count;

        _context.Update(stock);
    }
}
