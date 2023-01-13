using Fedorakin.CashDesk.Logic.Interfaces.Providers;

namespace Fedorakin.CashDesk.Logic.Providers;

public class TimeSpanProvider : ITimeSpanProvider
{
    public TimeSpan ForFiveMinutes()
    {
        return this.FromMinutes(5);
    }

    public TimeSpan FromMinutes(int count)
    {
        return TimeSpan.FromMinutes(count);
    }
}
