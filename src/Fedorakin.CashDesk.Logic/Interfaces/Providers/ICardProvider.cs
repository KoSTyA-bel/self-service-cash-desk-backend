using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Providers;

public interface ICardProvider : IBaseProvider<Card>
{
    public Task<Card?> GetCardByCode(string code, CancellationToken cancellationToken);
}
