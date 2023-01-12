using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Providers;

public interface IStockProvider : IBaseProvider<Stock>
{
    public Task<Stock?> GetStockForProduct(int productId, CancellationToken cancellationToken);
}
