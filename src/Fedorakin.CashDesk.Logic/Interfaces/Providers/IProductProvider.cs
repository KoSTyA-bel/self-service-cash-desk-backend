using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Providers;

public interface IProductProvider
{
    public Task<Product?> GetProduct(int id, CancellationToken cancellationToken);

    public Task<IEnumerable<Product>> GetRange(int page, int pageSize, CancellationToken cancellationToken);
}
