using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Providers;

public interface IProfileProvider
{
    public Task<Profile?> GetProdut(int id, CancellationToken cancellationToken);

    public Task<IEnumerable<Profile>> GetRange(int page, int pageSize, CancellationToken cancellationToken);
}
