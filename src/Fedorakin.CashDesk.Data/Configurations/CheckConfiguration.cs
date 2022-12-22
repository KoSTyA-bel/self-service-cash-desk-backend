using Fedorakin.CashDesk.Logic.Models;
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
        builder.HasOne<Cart>();
        builder.HasOne<SelfCheckout>();
        builder.HasOne<Card>();
        builder.Property(x => x.CardId).IsRequired(false);
    }
}
