using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Providers;

public interface ICashDescProvider
{
    public Task<SelfCheckout?> GetCashDesc(int id, CancellationToken cancellationToken);

    public Task<IEnumerable<SelfCheckout>> GetRange(int page, int pageSize, CancellationToken cancellationToken);
}
