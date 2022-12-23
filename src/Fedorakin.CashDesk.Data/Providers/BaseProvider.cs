using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public abstract class BaseProvider<T> : IBaseProvider<T> where T : BaseEntity
{
    protected DbSet<T> _data;

    public BaseProvider(DbSet<T> dataColletion)
    {
        _data = dataColletion ?? throw new ArgumentNullException(nameof(dataColletion));
    }

    public Task<T?> Get(int id, CancellationToken cancellationToken)
    {
        return _data.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<T>> GetRange(int page, int pageSize, CancellationToken cancellationToken)
    {
        return _data.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }
}
