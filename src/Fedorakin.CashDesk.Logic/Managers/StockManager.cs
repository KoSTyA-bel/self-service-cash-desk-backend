using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Constants;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

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

    public async Task<Stock?> GetByIdAsync(int id, params string[] includes)
    {
        var stocks = await GetRangeAsync(readIds: new ReadOnlyCollection<int>(new List<int> { id }), includes: includes);

        return stocks.SingleOrDefault();
    }

    public Task<List<Stock>> GetRangeAsync(
        int? page = default, 
        int? pageSize = default, 
        IReadOnlyCollection<int>? readIds = default, 
        IReadOnlyCollection<int>? readProductIds = default, 
        IReadOnlyCollection<string>? readNames = default, 
        IReadOnlyCollection<string>? readBarcodes = default, 
        params string[] includes)
    {
        var query = _context.Stocks.AsNoTracking();

        if (readIds is not null && readIds.Any())
        {
            query = query.Where(stock => readIds!.Contains(stock.Id));
        }

        if (readProductIds is not null && readProductIds.Any())
        {
            query = query.Where(stock => readProductIds!.Contains(stock.ProductId));
        }

        if (readNames is not null && readNames.Any())
        {
            query = query.Where(stock => stock.Product!.Name.Contains(readNames.First()));
        }

        if (readBarcodes is not null && readBarcodes.Any())
        {
            query = query.Where(stock => stock.Product!.Barcode.Contains(readBarcodes.First()));
        }

        if (includes.Contains(IncludeModels.StockNavigation.Product))
        {
            query = query.Include(stock => stock.Product);
        }

        if (page.HasValue && pageSize.HasValue)
        {
            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        return query.ToListAsync();
    }

    public async Task<Stock?> GetStockForProductAsync(int productId)
    {
        var stocks = await GetRangeAsync(readProductIds: new ReadOnlyCollection<int>(new List<int> { productId }));

        return stocks.SingleOrDefault();
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
