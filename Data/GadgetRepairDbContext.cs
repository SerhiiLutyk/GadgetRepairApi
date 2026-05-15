using GadgetRepairApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GadgetRepairApi.Data;

public class GadgetRepairDbContext(DbContextOptions<GadgetRepairDbContext> options) : DbContext(options)
{
    public DbSet<Gadget> Gadgets => Set<Gadget>();
    public DbSet<RepairOrder> RepairOrders => Set<RepairOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gadget>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Brand).HasMaxLength(100).IsRequired();
            entity.Property(g => g.Model).HasMaxLength(100).IsRequired();
            entity.Property(g => g.OwnerName).HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<RepairOrder>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.IssueDescription).HasMaxLength(1000).IsRequired();
            entity.Property(r => r.Price).HasPrecision(18, 2);

            entity.HasOne(r => r.Gadget)
                .WithMany(g => g.RepairOrders)
                .HasForeignKey(r => r.GadgetId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Gadget>().HasData(
            new Gadget { Id = 1, Brand = "Dell", Model = "Laptop", OwnerName = "Demo Customer" },
            new Gadget { Id = 2, Brand = "Apple", Model = "Smartphone", OwnerName = "Demo Customer" },
            new Gadget { Id = 3, Brand = "Samsung", Model = "Tablet", OwnerName = "Demo Customer" });

        modelBuilder.Entity<RepairOrder>().HasData(
            new RepairOrder
            {
                Id = 1,
                GadgetId = 1,
                IssueDescription = "Screen replacement",
                Status = RepairOrderStatus.InProgress,
                Price = 3500m
            },
            new RepairOrder
            {
                Id = 2,
                GadgetId = 2,
                IssueDescription = "Battery not charging",
                Status = RepairOrderStatus.Pending,
                Price = 1200m
            });
    }
}
