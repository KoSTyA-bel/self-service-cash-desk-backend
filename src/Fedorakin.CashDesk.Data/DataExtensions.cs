using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Providers;
using Fedorakin.CashDesk.Data.Repositories;
using Fedorakin.CashDesk.Logic.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fedorakin.CashDesk.Data;

public static class DataExtensions
{
    // TODO
    // Think of a way to get rid of repetition
    public static IServiceCollection AddDataBase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextPool<DataContext>(options => options.UseSqlServer(connectionString));

        services
            .AddScoped(provider =>
            {
                var service = provider.GetService(typeof(DataContext)) as DataContext;
                return service.Profiles;
            })
            .AddScoped(provider =>
            {
                var service = provider.GetService(typeof(DataContext)) as DataContext;
                return service.Products;
            })
            .AddScoped(provider =>
            {
                var service = provider.GetService(typeof(DataContext)) as DataContext;
                return service.Cards;
            })
            .AddScoped(provider =>
            {
                var service = provider.GetService(typeof(DataContext)) as DataContext;
                return service.SelfCheckouts;
            })
            .AddScoped(provider =>
            {
                var service = provider.GetService(typeof(DataContext)) as DataContext;
                return service.Checks;
            })
            .AddScoped(provider =>
            {
                var service = provider.GetService(typeof(DataContext)) as DataContext;
                return service.Roles;
            })
            .AddScoped(provider =>
            {
                var service = provider.GetService(typeof(DataContext)) as DataContext;
                return service.Carts;
            })
            .AddScoped(provider =>
            {
                var service = provider.GetService(typeof(DataContext)) as DataContext;
                return service.Stocks;
            });

        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IProfileRepository, ProfileRepository>()
            .AddScoped<IProfileProvider, ProfileProvider>()
            .AddScoped<IProductProvider, ProductProvider>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<ISelfCheckoutProvider, SelfCheckoutProvider>()
            .AddScoped<ISelfCheckoutRepository, SelfCheckoutRepository>()
            .AddScoped<IStockProvider, StockProvider>()
            .AddScoped<IStockRepository, StockRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IRoleProvider, RoleProvider>()
            .AddScoped<ICardProvider, CardProvider>()
            .AddScoped<ICardRepository, CardRepository>()
            .AddScoped<ICartProvider, CartProvider>()
            .AddScoped<ICartRepository, CartRepository>()
            .AddScoped<ICheckProvider, CheckProvider>()
            .AddScoped<ICheckRepository, CheckRepository>();

        return services;
    }
}
