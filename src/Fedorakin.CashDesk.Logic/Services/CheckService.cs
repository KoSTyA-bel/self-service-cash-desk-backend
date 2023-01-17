using Fedorakin.CashDesk.Logic.Interfaces;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Services;

public class CheckService : ServiceBase<Check>, ICheckService
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public CheckService(ICheckProvider provider, ICheckRepository repository, IDateTimeProvider dateTimeProvider, IUnitOfWork unitOfWork) 
        : base(provider, repository, unitOfWork)
    {
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public override Task Create(Check entity, CancellationToken cancellationToken)
    {
        entity.Date = _dateTimeProvider.Now();

        return base.Create(entity, cancellationToken);
    }
}
