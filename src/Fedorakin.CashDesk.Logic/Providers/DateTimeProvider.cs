using Fedorakin.CashDesk.Logic.Interfaces.Providers;

namespace Fedorakin.CashDesk.Logic.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime NexWeek()
    {
        return DateTime.Now.AddDays(7);
    }

    public DateTime Now()
    {
        return DateTime.UtcNow;
    }
}
