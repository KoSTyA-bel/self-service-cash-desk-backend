using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Managers;
using Fedorakin.CashDesk.Logic.Providers;
using Fedorakin.CashDesk.Logic.Services;
using Fedorakin.CashDesk.Web.Interfaces.Utils;
using Fedorakin.CashDesk.Web.Mapping;
using Fedorakin.CashDesk.Web.Middlewares;
using Fedorakin.CashDesk.Web.Settings;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("CashDesk");

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
builder.Services.AddSingleton<JwtSettings>(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);

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
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICheckService, CheckService>();
builder.Services.AddScoped<ISelfCheckoutService, SelfCheckoutService>();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(MyAllowSpecificOrigins,
//                      policy =>
//                      {
//                          policy.AllowAnyHeader();
//                          policy.AllowAnyMethod();
//                          policy.WithOrigins("http://localhost:3000");
//                      });
//});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseCors(config => config.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseMiddleware<ErrorHandler>();

app.UseMiddleware<JwtMiddleware>();

//app.UseHttpsRedirection();

app.MapControllers();


app.Run();
