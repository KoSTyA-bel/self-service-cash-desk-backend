using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Providers;

public interface IProfileProvider
{
    public Task<Person?> GetProfile(int id, CancellationToken cancellationToken);

    public Task<IEnumerable<Person>> GetRange(int page, int pageSize, CancellationToken cancellationToken);
}
