using Fedorakin.CashDesk.Logic.Interfaces;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Services;

public abstract class ServiceBase<T> : IBaseService<T> where T : BaseEntity
{
    protected readonly IBaseProvider<T> _provider;
    protected readonly IBaseRepository<T> _repository;
    protected readonly IUnitOfWork _unitOfWork;

    protected ServiceBase(IBaseProvider<T> provider, IBaseRepository<T> repository, IUnitOfWork unitOfWork)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public virtual async Task Create(T entity, CancellationToken cancellationToken)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await _repository.Create(entity, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);
    }

    public virtual async Task Delete(int id, CancellationToken cancellationToken)
    {
        var entity = await _provider.Get(id, cancellationToken);

        if (entity is null)
        {
            return;
        }

        await _repository.Delete(entity, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);
    }

    public virtual Task<T> Get(int id, CancellationToken cancellationToken)
    {
        return _provider.Get(id, cancellationToken);
    }

    public virtual Task<List<T>> GetRange(int page, int pageSize, CancellationToken cancellationToken)
    {
        return _provider.GetRange(page, pageSize, cancellationToken);
    }

    public virtual async Task Update(T entity, CancellationToken cancellationToken)
    {
        await _repository.Update(entity, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);
    }
}
