using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public class ProductProvider : IProductProvider
{
    private readonly DbSet<Product> _products;

    public ProductProvider(DbSet<Product> products)
    {
        _products = products ?? throw new ArgumentNullException(nameof(products));
    }

    public Task<Product?> GetProduct(int id, CancellationToken cancellationToken)
    {
        return _products.FirstOrDefaultAsync(product => product.Id == id, cancellationToken);
    }

    public Task<IEnumerable<Product>> GetRange(int page, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
