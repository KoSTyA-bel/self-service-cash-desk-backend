using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Providers;

public interface ICardProvider : IBaseProvider<Card>
{
    public Task<Card?> GetByProfileId(int id, CancellationToken cancellationToken);

    public Task<Card?> GetCardByCode(string code, CancellationToken cancellationToken);
}
