using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Repositories;

public class CardRepository : BaseRepository<Card>, ICardRepository
{
    public CardRepository(DbSet<Card> cards)
        : base(cards)
    {
    }
}
