using Fedorakin.CashDesk.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fedorakin.CashDesk.Data.Configurations;

public class CheckConfiguration : IEntityTypeConfiguration<Check>
{
    public void Configure(EntityTypeBuilder<Check> builder)
    {
        builder.HasKey(x=> x.Id);
        builder.Property(x => x.Total).IsRequired();
        builder.Property(x => x.Discount).IsRequired();
        builder.Property(x => x.Amount).IsRequired();
        builder.HasOne<Cart>().WithOne().HasPrincipalKey<Cart>(x => x.Number).IsRequired();
        builder.HasOne(x => x.Card).WithMany().IsRequired(false);
    }
}
