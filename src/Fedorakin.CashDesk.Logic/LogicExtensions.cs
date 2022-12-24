using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Services;

namespace Fedorakin.CashDesk.Logic;

public static class LogicExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, string connectionString)
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
