using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Repositories;

public interface IProfileRepository
{
    public Task CreateProduct(Profile profile, CancellationToken cancellationToken);

    public Task UpdateProduct(Profile profile, CancellationToken cancellationToken);

    public Task DeleteProfile(int id, CancellationToken cancellationToken);
}
