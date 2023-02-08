using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Managers;

public interface ICardManager
{
    Task<Card?> GetByIdAsync(int id);

    Task<Card?> GetByProfileIdAsync(int id);

    Task<Card?> GetByCodeAsync(string code);

    Task<List<Card>> GetRangeAsync(int page, int pageSize);

    Task AddAsync(Card model);

    Task DeleteAsync(Card model);

    Task UpdateAsync(Card model);
}
