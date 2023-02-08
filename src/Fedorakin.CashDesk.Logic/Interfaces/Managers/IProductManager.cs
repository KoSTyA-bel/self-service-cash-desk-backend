using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Managers;

public interface IProductManager
{
    Task<Product?> GetByIdAsync(int id);

    Task<List<Product>> GetRangeAsync(int page, int pageSize, string name, string barcode);

    Task AddAsync(Product model);

    Task DeleteAsync(Product model);

    Task UpdateAsync(Product model);
}
