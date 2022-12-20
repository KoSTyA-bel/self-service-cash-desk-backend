using Fedorakin.CashDesk.Data.Configurations;
using Fedorakin.CashDesk.Logic.Interfaces;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Logic.Contexts;

public class DataContext : DbContext, IDataContext
{
	public DataContext(DbContextOptions<DataContext> options)
		: base(options)
	{
	}

    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CashDesc> CashDescs { get; set; }

    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProfileConfiguration());
        modelBuilder.ApplyConfiguration(new CardConfiguration());
        modelBuilder.ApplyConfiguration(new CashDescConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
