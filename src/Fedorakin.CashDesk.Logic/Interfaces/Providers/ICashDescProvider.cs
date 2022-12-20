using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Providers;

public interface ICashDescProvider
{
    public Task<CashDesc?> GetProduct(int id, CancellationToken cancellationToken);

    public Task<IEnumerable<CashDesc>> GetRange(int page, int pageSize, CancellationToken cancellationToken);
}
