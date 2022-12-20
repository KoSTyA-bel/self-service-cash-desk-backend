using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fedorakin.CashDesk.Data.Configurations;

public class CashDescConfiguration : IEntityTypeConfiguration<CashDesc>
{
    public void Configure(EntityTypeBuilder<CashDesc> builder)
    {
        builder.HasKey(desc => desc.Id);

        builder.Property(desc => desc.IsFree).IsRequired();
    }
}
