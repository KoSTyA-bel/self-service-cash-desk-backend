using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Managers;

public interface IProductManager
{
    Task<Product?> GetByIdAsync(int id);

    Task<List<Product>> GetRangeAsync(
        int? page = default, 
        int? pageSize = default, 
        IReadOnlyCollection<int>? readIds = default,
        IReadOnlyCollection<string>? readNames = default,
        IReadOnlyCollection<string>? readBarcode = default,
        params string[] includes);

    Task AddAsync(Product model);

    Task DeleteAsync(Product model);

    Task UpdateAsync(Product model);
}
