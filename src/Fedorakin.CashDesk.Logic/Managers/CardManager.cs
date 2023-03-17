using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Constants;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Logic.Managers;

public class CardManager : ICardManager
{
    private readonly DataContext _context;

    public CardManager(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task AddAsync(Card model)
    {
        return _context.Cards.AddAsync(model).AsTask();
    }

    public Task DeleteAsync(Card model)
    {
        return Task.FromResult(_context.Cards.Remove(model));
    }

    public Task<List<Card>> GetRangeAsync(
        int? page = default,
        int? pageSize = default,
        IReadOnlyCollection<int>? readOnlyIds = default,
        IReadOnlyCollection<int>? readOnlyProfileIds = default,
        IReadOnlyCollection<string>? readOnlyCodes = default,
        params string[] includes)
    {
        var query = _context.Cards.AsQueryable();

        if (readOnlyIds is not null && readOnlyIds.Any())
        {
            query = query.Where(card => readOnlyIds!.Contains(card.Id));
        }

        if (readOnlyProfileIds is not null && readOnlyProfileIds.Any())
        {
            query = query.Where(card => readOnlyProfileIds!.Contains(card.ProfileId));
        }

        if (readOnlyCodes is not null && readOnlyCodes.Any())
        {
            query = query.Where(card => readOnlyCodes!.Contains(card.Code));
        }

        if (includes.Contains(IncludeModels.CardNavigation.Profile))
        {
            query = query.Include(cart => cart.Profile);
        }

        if (includes.Contains(IncludeModels.CardNavigation.ProfileWithRole))
        {
            query = query.Include(cart => cart.Profile)
                .ThenInclude(profile => profile!.Role);
        }

        if (page.HasValue && pageSize.HasValue)
        {
            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        return query.ToListAsync();
    }

    public async Task UpdateAsync(Card model)
    {
        var card = await _context.Cards.FindAsync(model.Id);
        if (card is null)
        {
            return;
        }

        if (card.Discount != model.Discount)
        {
            card.Discount = model.Discount;
        }

        if (card.ProfileId != model.ProfileId)
        {
            card.ProfileId = model.ProfileId;
        }

        _context.Cards.Update(card);
    }
}
