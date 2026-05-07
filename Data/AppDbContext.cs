using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WoodX.API.Models;

namespace WoodX.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasIndex(u => u.Email).IsUnique();
        });

        modelBuilder.Entity<Product>(e =>
        {
            e.Property(p => p.Price).HasColumnType("decimal(10,2)");
            e.Property(p => p.OldPrice).HasColumnType("decimal(10,2)");
            e.Property(p => p.Tags).HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>(),
                new ValueComparer<List<string>>(
                    (a, b) => a != null && b != null && a.SequenceEqual(b),
                    c => c.Aggregate(0, (h, s) => HashCode.Combine(h, s.GetHashCode())),
                    c => c.ToList()));
        });

        modelBuilder.Entity<Order>(e =>
        {
            e.Property(o => o.Total).HasColumnType("decimal(10,2)");
            e.OwnsOne(o => o.ShippingAddress, sa => sa.ToJson());
            e.HasMany(o => o.Items).WithOne().HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderItem>(e =>
        {
            e.Property(i => i.Price).HasColumnType("decimal(10,2)");
        });
    }
}
