using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Managers;

public interface ICartManager
{
    Task<List<Cart>> GetRangeAsync(
        int? page = default,
        int? pageSize = default,
        IReadOnlyCollection<int>? readOnlyIds = default,
        IReadOnlyCollection<Guid>? readOnlyNumbers = default,
        params string[] includes);

    Task<Cart?> GetByIdAsync(int id);

    Task<Cart?> GetByNumberAsync(Guid number);

    Task AddAsync(Cart model);
}
