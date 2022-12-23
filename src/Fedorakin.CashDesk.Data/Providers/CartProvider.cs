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
}
