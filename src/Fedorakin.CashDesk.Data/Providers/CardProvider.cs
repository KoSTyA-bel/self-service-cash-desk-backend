using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public class CardProvider : BaseProvider<Card>, ICardProvider
{
    public CardProvider(DbSet<Card> cards)
        : base(cards)
    {
    }
}
