using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public class CardProvider : ICardProvider
{
    private readonly DbSet<Card> _cards;

    public CardProvider(DbSet<Card> cards)
    {
        _cards = cards ?? throw new ArgumentNullException(nameof(cards));
    }

    public Task<Card?> GetCard(int id, CancellationToken cancellationToken)
    {
        return _cards.FirstOrDefaultAsync(card => card.Id== id, cancellationToken);
    }

    public Task<IEnumerable<Card>> GetRange(int page, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
