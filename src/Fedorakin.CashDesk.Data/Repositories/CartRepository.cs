using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Repositories;

public class CartRepository : BaseRepository<Cart>, ICartRepository
{
	public CartRepository(DbSet<Cart> carts)
		: base(carts)
	{
	}
}
