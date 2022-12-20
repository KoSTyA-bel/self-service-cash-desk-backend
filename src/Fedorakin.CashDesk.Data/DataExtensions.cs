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
                return service.CashDescs;
            });

        services
            .AddScoped<IDataContext, DataContext>()
            .AddScoped<IProfileRepository, ProfileRepository>()
            .AddScoped<IProfileProvider, ProfileProvider>()
            .AddScoped<IProductProvider, ProductProvider>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<ICashDescProvider, CashDescProvider>()
            .AddScoped<ICashDescRepository, CashDescRepository>()
            .AddScoped<ICardProvider, CardProvider>()
            .AddScoped<ICardRepository, CardRepository>();

        return services;
    }
}
