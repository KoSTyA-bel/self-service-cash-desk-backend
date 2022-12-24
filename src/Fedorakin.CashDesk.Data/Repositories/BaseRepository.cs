using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected DbSet<T> _data;

    public BaseRepository(DbSet<T> dataColletion)
    {
        _data = dataColletion ?? throw new ArgumentNullException(nameof(dataColletion));
    }

    public virtual Task Create(T entity, CancellationToken cancellationToken)
    {
        return _data.AddAsync(entity).AsTask();
    }

    public virtual Task Delete(T entity, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.Remove(entity));
    }

    public virtual Task Update(T entity, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.Update(entity));
    }
}
