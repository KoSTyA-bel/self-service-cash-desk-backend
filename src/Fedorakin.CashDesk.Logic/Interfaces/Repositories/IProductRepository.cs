using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Repositories;

public interface IProductRepository
{
    public Task CreateProduct(Product product, CancellationToken cancellationToken);

    public Task UpdateProduct(Product product, CancellationToken cancellationToken);

    public Task DeleteProduct(Product product, CancellationToken cancellationToken);
}
