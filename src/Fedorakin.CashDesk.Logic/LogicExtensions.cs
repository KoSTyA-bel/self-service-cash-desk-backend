﻿using Microsoft.Extensions.DependencyInjection;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Services;

namespace Fedorakin.CashDesk.Logic;

public static class LogicExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IProductService, ProductService>()
            .AddScoped<IRoleService, RoleService>()
            .AddScoped<IProfileService, ProfileService>()
            .AddScoped<IStockService, StockService>()
            .AddScoped<ICartService, CartService>()
            .AddScoped<ICardService, CardService>()
            .AddScoped<ICheckService, CheckService>()
            .AddScoped<ISelfCheckoutService, SelfCheckoutService>();

        return services;
    }
}
