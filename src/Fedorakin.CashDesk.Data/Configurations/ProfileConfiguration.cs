using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fedorakin.CashDesk.Data.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(profile => profile.Id);
        builder.Property(profile => profile.CardId).IsRequired();
        builder.HasOne<Card>();
    }
}
