using Fedorakin.CashDesk.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataBase("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");

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
