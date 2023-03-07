using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Managers;

public interface IProfileManager
{
    Task<Profile?> GetByIdAsync(int id);

    Task<List<Profile>> GetRangeAsync(
        int? page = default, 
        int? pageSize = default,
        IReadOnlyCollection<int>? readIds = default,
        params string[] includes);

    Task AddAsync(Profile model);

    Task DeleteAsync(Profile model);

    Task UpdateAsync(Profile model);
}
