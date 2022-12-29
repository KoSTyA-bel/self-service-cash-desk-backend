using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public class SelfCheckoutProvider : BaseProvider<SelfCheckout>, ISelfCheckoutProvider
{
    public SelfCheckoutProvider(DbSet<SelfCheckout> cashDescs)
        : base(cashDescs)
    {
    }

    public override Task<SelfCheckout?> Get(int id, CancellationToken cancellationToken)
    {
        return _data.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
