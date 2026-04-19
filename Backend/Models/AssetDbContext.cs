using Microsoft.EntityFrameworkCore;
namespace Backend.Models;

public class AssetDbContext : DbContext
{
    public AssetDbContext(DbContextOptions<AssetDbContext> options) : base(options) {}

    public DbSet<Asset> Assets { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
}