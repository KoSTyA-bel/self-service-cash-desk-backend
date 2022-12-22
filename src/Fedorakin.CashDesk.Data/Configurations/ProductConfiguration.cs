using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fedorakin.CashDesk.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Desciprion).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Barcode).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Weight).IsRequired();
    }
}
