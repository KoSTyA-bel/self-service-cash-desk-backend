using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Managers;

public interface ICardManager
{
    Task<List<Card>> GetRangeAsync(
        int? page = default,
        int? pageSize = default,
        IReadOnlyCollection<int>? readOnlyIds = default,
        IReadOnlyCollection<int>? readOnlyProfileIds = default,
        IReadOnlyCollection<string>? readOnlyCodes = default,
        params string[] includes);

    Task AddAsync(Card model);

    Task DeleteAsync(Card model);

    Task UpdateAsync(Card model);
}
