using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Providers;

public interface IBaseProvider<T> where T : BaseEntity
{
    public Task<T?> Get(int id, CancellationToken cancellationToken);

    public Task<List<T>> GetRange(int page, int pageSize, CancellationToken cancellationToken);
}
