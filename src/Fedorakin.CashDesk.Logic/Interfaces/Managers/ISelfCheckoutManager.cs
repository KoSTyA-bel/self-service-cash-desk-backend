using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Managers;

public interface ISelfCheckoutManager
{
    Task<SelfCheckout?> GetByIdAsync(int id);

    Task<List<SelfCheckout>> GetRangeAsync(int? page = default, int? pageSize = default, IReadOnlyCollection<int>? readIds = default);

    Task AddAsync(SelfCheckout model);

    Task DeleteAsync(SelfCheckout model);

    Task UpdateAsync(SelfCheckout model);
}
