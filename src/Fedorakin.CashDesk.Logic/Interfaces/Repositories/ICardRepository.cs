using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Repositories;

public interface ICardRepository
{
    public Task CreateCard(Card card, CancellationToken cancellationToken);

    public Task UpdateCard(Card card, CancellationToken cancellationToken);

    public Task DeleteCard(Card card, CancellationToken cancellationToken);
}
