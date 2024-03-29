﻿using Fedorakin.CashDesk.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fedorakin.CashDesk.Data.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Number).IsRequired();
        builder.HasMany(x => x.Products).WithMany();
    }
}
