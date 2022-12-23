namespace Fedorakin.CashDesk.Logic.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    public Task Create(T entity, CancellationToken cancellationToken);

    public Task Update(T entity, CancellationToken cancellationToken);

    public Task Delete(T entity, CancellationToken cancellationToken);
}
