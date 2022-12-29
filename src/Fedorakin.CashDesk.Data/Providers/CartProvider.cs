using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public class CartProvider : BaseProvider<Cart>, ICartProvider
{
	public CartProvider(DbSet<Cart> carts)
		: base(carts)
	{
	}

    public override Task<Cart?> Get(int id, CancellationToken cancellationToken)
    {
        return _data
            .Where(x => x.Id == id)
            .Include(x => x.Products)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
