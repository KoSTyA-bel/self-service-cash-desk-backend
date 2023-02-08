using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Managers;

public interface ICheckManager
{
    Task<Check?> GetByIdAsync(int id);

    Task<List<Check>> GetRangeAsync(int page, int pageSize);

    Task AddAsync(Check model);
}
