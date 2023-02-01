using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Logic.Managers;

public class ProductManager : IProductManager
{
    private readonly DataContext _context;

    public ProductManager(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task AddAsync(Product model)
    {
        return _context.Products.AddAsync(model).AsTask();
    }

    public Task DeleteAsync(Product model)
    {
        return Task.FromResult(_context.Products.Remove(model));
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        return _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id== id);
    }

    public Task<List<Product>> GetRangeAsync(int page, int pageSize, string name, string barcode)
    {
        return _context.Products
            .AsNoTracking()
            .Where(x => x.Name.Contains(name) && x.Barcode.Contains(barcode))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task UpdateAsync(Product model)
    {
        var product = await GetByIdAsync(model.Id);

        if (product is null)
        {
            return;
        }

        product.Name = model.Name;
        product.Price = model.Price;
        product.Description = model.Description;
        product.Weight = model.Weight;
        product.Barcode = model.Barcode;

        _context.Products.Update(product);
    }
}
