using Microsoft.Extensions.DependencyInjection;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Services;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Providers;

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
            .AddScoped<ISelfCheckoutService, SelfCheckoutService>()
            .AddScoped<IDateTimeProvider, DateTimeProvider>()
            .AddScoped<ITimeSpanProvider, TimeSpanProvider>();

        return services;
    }
}
