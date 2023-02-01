using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Services;

namespace Fedorakin.CashDesk.Logic.Services;

public class CheckService : ICheckService
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public CheckService(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public void SetDateTime(Check check)
    {
        check.Date = _dateTimeProvider.Now();
    }
}
