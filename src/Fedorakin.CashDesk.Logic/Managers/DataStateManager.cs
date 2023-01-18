using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;

namespace Fedorakin.CashDesk.Logic.Managers;

public class DataStateManager : IDataStateManager
{
    private readonly DataContext _context;

    public DataStateManager(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task CommitChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
