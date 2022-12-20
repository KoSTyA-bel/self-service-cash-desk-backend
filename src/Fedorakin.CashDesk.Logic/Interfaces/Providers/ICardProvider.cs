using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Providers;

public interface ICardProvider
{
    public Task<Card?> GetProduct(int id, CancellationToken cancellationToken);

    public Task<IEnumerable<Card>> GetRange(int page, int pageSize, CancellationToken cancellationToken);
}
