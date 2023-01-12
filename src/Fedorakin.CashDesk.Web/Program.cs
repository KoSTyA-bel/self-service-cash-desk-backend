using Fedorakin.CashDesk.Data;
using Fedorakin.CashDesk.Logic;
using Fedorakin.CashDesk.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataBase("Data Source=(localdb)\\MyInstance;Database=myDataBase");

builder.Services.AddMemoryCache();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddServices();

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
