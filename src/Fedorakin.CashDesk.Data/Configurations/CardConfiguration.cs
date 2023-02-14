using Fedorakin.CashDesk.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fedorakin.CashDesk.Data.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).IsFixedLength().HasMaxLength(16);
        builder.Property(x => x.CVV).IsFixedLength().HasMaxLength(3);
    }
}
