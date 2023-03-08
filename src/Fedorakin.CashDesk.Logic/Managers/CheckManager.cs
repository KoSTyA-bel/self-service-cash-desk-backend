using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Constants;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Fedorakin.CashDesk.Logic.Managers;

public class CheckManager : ICheckManager
{
    private readonly DataContext _context;

    public CheckManager(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task AddAsync(Check model)
    {
        return _context.Checks.AddAsync(model).AsTask();
    }

    public async Task<Check?> GetByIdAsync(int id)
    {
        var checks = await GetRangeAsync(readCheckIds: new ReadOnlyCollection<int>(new List<int> { id }));

        return checks.SingleOrDefault();
    }

    public Task<List<Check>> GetRangeAsync(
        int? page = null, 
        int? pageSize = null,
        IReadOnlyCollection<int>? readCheckIds = null,
        IReadOnlyCollection<string>? readCardCodes = null, 
        IReadOnlyCollection<string>? readCardCVVs = null, 
        params string[] includes)
    {
        var query = _context.Checks.AsNoTracking();

        if (readCheckIds is not null && readCheckIds.Any())
        {
            query = query.Where(check => readCheckIds!.Contains(check.Id));
        }

        if (readCardCodes is not null && readCardCodes.Any())
        {
            query = query.Where(check => readCardCodes!.Contains(check.Card!.Code));
        }

        if (readCardCVVs is not null && readCardCVVs.Any())
        {
            query = query.Where(check => readCardCVVs!.Contains(check.Card!.CVV));
        }

        if (includes.Contains(IncludeModels.CheckNavigation.SelfCheckout))
        {
            query = query.Include(check => check.SelfCheckout);
        }

        if (includes.Contains(IncludeModels.CheckNavigation.Card))
        {
            query = query.Include(check => check.Card);
        }

        if (includes.Contains(IncludeModels.CheckNavigation.CardWithProfile))
        {
            query = query
                .Include(check => check.Card)
                .ThenInclude(card => card!.Profile);
        }

        if (includes.Contains(IncludeModels.CheckNavigation.CardWithProfileWithRole))
        {
            query = query
                .Include(check => check.Card)
                .ThenInclude(card => card!.Profile)
                .ThenInclude(profile => profile!.Role);
        }

        if (page.HasValue && pageSize.HasValue)
        {
            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        return query.ToListAsync();
    }
}
