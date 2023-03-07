using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Managers;

public interface IStockManager
{
    Task<Stock?> GetByIdAsync(int id, params string[] includes);

    Task<Stock?> GetStockForProductAsync(int productId);

    Task<List<Stock>> GetRangeAsync(
        int? page = default, 
        int? pageSize = default,
        IReadOnlyCollection<int>? readIds = default,
        IReadOnlyCollection<int>? readProductIds = default,
        IReadOnlyCollection<string>? readNames = default, 
        IReadOnlyCollection<string>? readBarcodes = default,
        params string[] includes);

    Task AddAsync(Stock model);

    Task DeleteAsync(Stock model);

    Task UpdateAsync(Stock model);
}
