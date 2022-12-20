using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task CreateProduct(Product product, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProduct(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProduct(Product product, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
