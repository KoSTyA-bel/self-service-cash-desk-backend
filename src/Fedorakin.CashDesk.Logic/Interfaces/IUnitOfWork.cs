namespace Fedorakin.CashDesk.Logic.Interfaces;

public interface IUnitOfWork
{
    public Task SaveChanges(CancellationToken cancellationToken);
}
