using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Repositories;

public class CardRepository : ICardRepository
{
    private readonly DbSet<Card> _cards;

    public CardRepository(DbSet<Card> cards)
    {
        _cards = cards ?? throw new ArgumentNullException(nameof(cards));
    }

    public Task CreateCard(Card card, CancellationToken cancellationToken)
    {
        return _cards.AddAsync(card, cancellationToken).AsTask();
    }

    public Task DeleteCard(Card card, CancellationToken cancellationToken)
    {
        return Task.FromResult(_cards.Remove(card));
    }

    public Task UpdateCard(Card card, CancellationToken cancellationToken)
    {
        return Task.FromResult(_cards.Update(card));
    }
}
