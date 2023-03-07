using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Managers;

public interface IRoleManager
{
    Task<Role?> GetByIdAsync(int id);

    Task<List<Role>> GetRangeAsync(
        int? page = default, 
        int? pageSize = default,
        IReadOnlyCollection<int>? readIds = default);

    Task AddAsync(Role model);

    Task DeleteAsync(Role model);

    Task UpdateAsync(Role model);
}
