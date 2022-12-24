using Fedorakin.CashDesk.Data.Configurations;
using Fedorakin.CashDesk.Logic.Interfaces;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Logic.Contexts;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options)
		: base(options)
	{
	}

    public DbSet<Profile> Profiles { get; set; }

    public DbSet<Card> Cards { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<SelfCheckout> SelfCheckouts { get; set; }

    public DbSet<Cart> Carts { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Stock> Stocks { get; set; }

    public DbSet<Check> Checks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProfileConfiguration());
        modelBuilder.ApplyConfiguration(new CardConfiguration());
        modelBuilder.ApplyConfiguration(new SelfCheckoutConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new StockConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new CheckConfiguration());
        modelBuilder.ApplyConfiguration(new CartConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
