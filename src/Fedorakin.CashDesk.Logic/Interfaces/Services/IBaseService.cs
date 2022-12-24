namespace Fedorakin.CashDesk.Logic.Interfaces.Services;

public interface IBaseService<T> where T : class
{
    public Task Create(T entity, CancellationToken cancellationToken);

    public Task Update(T entity, CancellationToken cancellationToken);

    public Task Delete(int id, CancellationToken cancellationToken);

    public Task<T> Get(int id, CancellationToken cancellationToken);

    public Task<List<T>> GetRange(int page, int pageSize, CancellationToken cancellationToken);
}
