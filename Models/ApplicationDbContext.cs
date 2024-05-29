using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MiniProjet.Net.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Commande> Commandes { get; set; }

        public DbSet<Panier> Paniers { get; set; }
        public DbSet<ContenuPanier> ContenuPanier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration pour Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price)
                      .HasColumnType("decimal(18,2)");
            });

            // Configuration pour OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(oi => oi.Price)
                      .HasColumnType("decimal(18,2)");
            });

            // Configuration pour Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.TotalAmount)
                      .HasColumnType("decimal(18,2)");
            });

            // Configuration des relations
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);
        }
    }
}
