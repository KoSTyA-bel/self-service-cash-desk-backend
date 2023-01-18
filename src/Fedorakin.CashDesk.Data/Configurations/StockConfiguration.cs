using Fedorakin.CashDesk.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fedorakin.CashDesk.Data.Configurations;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Count).IsRequired();
        builder.HasOne(x => x.Product).WithOne().HasForeignKey(nameof(Stock), "ProductId").IsRequired();
    }
}
