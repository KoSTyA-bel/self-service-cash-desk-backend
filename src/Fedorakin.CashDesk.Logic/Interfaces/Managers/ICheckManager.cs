using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Managers;

public interface ICheckManager
{
    Task<Check?> GetByIdAsync(int id);

    Task<List<Check>> GetRangeAsync(
        int? page = default,
        int? pageSize = default,
        IReadOnlyCollection<int>? readCheckIds = default,
        IReadOnlyCollection<string>? readCardCodes = default,
        IReadOnlyCollection<string>? readCardCVVs = default,
        params string[] includes);

    Task AddAsync(Check model);
}
