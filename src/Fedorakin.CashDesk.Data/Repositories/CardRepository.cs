using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Data.Repositories;

public class CardRepository : ICardRepository
{
    private readonly DataContext _context;

    public CardRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task CreateCard(Card card, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCard(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCard(Card card, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
