using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Repositories;

public class SelfCheckoutRepository : BaseRepository<SelfCheckout>, ISelfCheckoutRepository
{
    public SelfCheckoutRepository(DbSet<SelfCheckout> selfCheckout)
        : base(selfCheckout)
    {
    }
}
