using Fedorakin.CashDesk.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Logic.Contexts;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options)
		: base(options)
	{
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
