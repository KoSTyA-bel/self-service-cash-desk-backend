﻿using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Providers;

public class ProductProvider : BaseProvider<Product>, IProductProvider
{

    public ProductProvider(DbSet<Product> products)
        : base(products)
    {
    }
}
