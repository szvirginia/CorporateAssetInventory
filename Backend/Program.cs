using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// CORS configuration to allow requests from any origin, method, and header
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

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
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Controllers path
app.MapControllers();

// Ensure database is created/migrated and seed initial data if empty
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<AssetDbContext>();
        db.Database.Migrate();

        // seed if empty
        if (!db.Assets.Any())
        {
            db.Assets.AddRange(
                new Asset { AssetName = "Laptop - Default A", SerialNumber = "DEF-A-0001", Type = AssetType.InStock },
                new Asset { AssetName = "Monitor - Default B", SerialNumber = "DEF-B-0002", Type = AssetType.InStock },
                new Asset { AssetName = "Keyboard - Default C", SerialNumber = "DEF-C-0003", Type = AssetType.InStock }
            );
            db.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

app.Run();