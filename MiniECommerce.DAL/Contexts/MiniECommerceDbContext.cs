using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.DAL.Contexts
{
    public class MiniECommerceDbContext: IdentityDbContext<User,IdentityRole<Guid>,Guid>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }

        public MiniECommerceDbContext(DbContextOptions<MiniECommerceDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);
            });

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.ToTable("Baskets");
                entity.HasKey(b => b.Id);

                entity.HasOne(b => b.User)
                    .WithOne()
                    .HasForeignKey<Basket>(b => b.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(c => c.Id);

                entity.HasMany(c => c.Products)
                    .WithOne(p => p.Category)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.HasMany(b => b.Products)
                    .WithMany()
                    .UsingEntity<Dictionary<string, object>>(
                        "BasketProduct",
                        j => j.HasOne<Product>()
                              .WithMany()
                              .HasForeignKey("ProductId")
                              .OnDelete(DeleteBehavior.Cascade),
                        j => j.HasOne<Basket>()
                              .WithMany()
                              .HasForeignKey("BasketId")
                              .OnDelete(DeleteBehavior.Cascade),
                        j =>
                        {
                            j.ToTable("BasketProducts");
                            j.HasKey("BasketId", "ProductId");
                        });
            });
        }


    }
}
