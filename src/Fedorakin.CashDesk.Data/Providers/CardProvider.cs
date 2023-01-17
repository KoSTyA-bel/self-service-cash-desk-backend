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

    public Task<Card?> GetByProfileId(int id, CancellationToken cancellationToken)
    {
        return IncludeNavigationEntities(_data)
            .FirstOrDefaultAsync(x => x.ProfileId == id, cancellationToken);
    }

    public Task<Card?> GetCardByCode(string code, CancellationToken cancellationToken)
    {
        return IncludeNavigationEntities(_data)
            .FirstOrDefaultAsync(x => x.Code.Equals(code), cancellationToken);
    }

    protected override IQueryable<Card> IncludeNavigationEntities(IQueryable<Card> data)
    {
        return data
            .Include(x => x.Profile)
            .ThenInclude(x => x.Role);
    }
}
