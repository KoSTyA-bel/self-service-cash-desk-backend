namespace Fedorakin.CashDesk.Logic.Interfaces.Providers;

public interface ITimeSpanProvider
{
    public TimeSpan FromMinutes(int count);

    public TimeSpan ForFiveMinutes();
}
