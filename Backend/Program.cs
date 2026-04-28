using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Database
var connectionString = builder.Configuration.GetConnectionString("defaultConnection");
builder.Services.AddDbContext<AssetDbContext>(options =>
    options.UseMySql(connectionString, serverVersion: ServerVersion.AutoDetect(connectionString)));

// Controllers
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Controllers path
app.MapControllers();

app.Run();