using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
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

    public Task<Card?> GetByCodeAsync(string code)
    {
        return _context.Cards
            .Include(x => x.Profile)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Code.Equals(code));
    }

    public Task<Card?> GetByIdAsync(int id)
    {
        return _context.Cards
            .Include(x => x.Profile)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Card?> GetByProfileIdAsync(int id)
    {
        return _context.Cards
            .Include(x => x.Profile)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.ProfileId == id);
    }

    public Task<List<Card>> GetRangeAsync(int page, int pageSize)
    {
        return _context.Cards
            .AsNoTracking()
            .Include(x => x.Profile)
            .ThenInclude(x => x.Role)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task UpdateAsync(Card model)
    {
        var card = await GetByIdAsync(model.Id);

        if (card is null)
        {
            return;
        }

        card.Discount = model.Discount;
        card.ProfileId = model.ProfileId;

        _context.Cards.Update(card);
    }
}
