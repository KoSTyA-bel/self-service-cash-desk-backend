using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fedorakin.CashDesk.Data.Configurations;

public class SelfCheckoutConfiguration : IEntityTypeConfiguration<SelfCheckout>
{
    public void Configure(EntityTypeBuilder<SelfCheckout> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsBusy).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
    }
}
