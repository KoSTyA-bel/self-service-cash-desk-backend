using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fedorakin.CashDesk.Data.Configurations;

public class CashDescConfiguration : IEntityTypeConfiguration<SelfCheckout>
{
    public void Configure(EntityTypeBuilder<SelfCheckout> builder)
    {
        builder.HasKey(desc => desc.Id);

        builder.Property(desc => desc.IsBusy).IsRequired();
    }
}
