using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Repositories;

public interface IProfileRepository
{
    public Task CreateProduct(Person profile, CancellationToken cancellationToken);

    public Task UpdateProduct(Person profile, CancellationToken cancellationToken);

    public Task DeleteProfile(Person profile, CancellationToken cancellationToken);
}
