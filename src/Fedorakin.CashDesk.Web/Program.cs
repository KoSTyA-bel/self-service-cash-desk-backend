using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Managers;
using Fedorakin.CashDesk.Logic.Providers;
using Fedorakin.CashDesk.Logic.Services;
using Fedorakin.CashDesk.Web.Mapping;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CashDesk");

builder.Services.AddDbContextPool<DataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<ICartManager, CartManager>();
builder.Services.AddScoped<ICardManager, CardManager>();
builder.Services.AddScoped<IProfileManager, ProfileManager>();
builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped<IStockManager, StockManager>();
builder.Services.AddScoped<IRoleManager, RoleManager>();
builder.Services.AddScoped<ISelfCheckoutManager, SelfCheckoutManager>();
builder.Services.AddScoped<ICheckManager, CheckManager>();
builder.Services.AddScoped<IDataStateManager, DataStateManager>();

builder.Services.AddScoped<ITimeSpanProvider, TimeSpanProvider>();
builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
