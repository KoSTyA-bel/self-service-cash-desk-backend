using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Services;

public interface IStockService : IBaseService<Stock>
{
    public Task<Stock?> GetStockForProduct(int productId, CancellationToken cancellationToken);
}
