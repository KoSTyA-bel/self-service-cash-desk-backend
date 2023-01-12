using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Services;

public interface ICardService : IBaseService<Card>
{
    public Task<Card?> GetCardByCode(string code, CancellationToken cancellationToken);
}
