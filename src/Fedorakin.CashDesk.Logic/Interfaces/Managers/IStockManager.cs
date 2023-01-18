using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Managers;

public interface IStockManager
{
    Task<Stock?> GetByIdAsync(int id);

    Task<Stock?> GetStockForProductAsync(int productId);

    Task<List<Stock>> GetRangeAsync(int page, int pageSize);

    Task AddAsync(Stock model);

    Task DeleteAsync(Stock model);

    Task UpdateAsync(Stock model);
}
