namespace Fedorakin.CashDesk.Logic.Interfaces;

public interface IDataContext
{
    public Task SaveChanges(CancellationToken cancellationToken);
}
