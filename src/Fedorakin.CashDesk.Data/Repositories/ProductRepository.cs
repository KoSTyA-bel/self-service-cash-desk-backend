using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DbSet<Product> _products;

    public ProductRepository(DbSet<Product> products)
    {
        _products = products ?? throw new ArgumentNullException(nameof(products));
    }

    public Task CreateProduct(Product product, CancellationToken cancellationToken)
    {
        return _products.AddAsync(product, cancellationToken).AsTask();
    }

    public Task DeleteProduct(Product product, CancellationToken cancellationToken)
    {
        return Task.FromResult(_products.Remove(product));
    }

    public Task UpdateProduct(Product product, CancellationToken cancellationToken)
    {
        return Task.FromResult(_products.Update(product));
    }
}
