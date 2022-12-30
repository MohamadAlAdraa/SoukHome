using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SoukHome.Models;

namespace SoukHome.Data;

public partial class SoukHomeDbContext : DbContext
{
    public SoukHomeDbContext()
    {
    }

    public SoukHomeDbContext(DbContextOptions<SoukHomeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Basket> Baskets { get; set; }

    public virtual DbSet<BasketOrder> BasketOrders { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SoukHomeDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => new { e.AdminId, e.Email }).HasName("PK__Admin__E7B3E6B007984CDB");

            entity.Property(e => e.AdminId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Basket>(entity =>
        {
            entity.HasKey(e => new { e.BasketId, e.CustomerBasketEmailId }).HasName("PK__Basket__3651DE524C1E0EDF");

            entity.HasOne(d => d.Customer).WithOne(p => p.Basket).HasConstraintName("FK_Basket_ToCustomer");
        });

        modelBuilder.Entity<BasketOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__BasketOr__0809335DE3936D4F");

            entity.HasOne(d => d.Product).WithMany(p => p.BasketOrders).HasConstraintName("FK_Basket_ToProduct");

            entity.HasOne(d => d.Basket).WithMany(p => p.BasketOrders).HasConstraintName("FK_Basket_ToBasket");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.Email }).HasName("PK__Customer__FCA72D6B9CC3AE54");

            entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => new { e.OfferId, e.ProductId }).HasName("PK__Offer__EA4CE716915FD3B0");

            entity.Property(e => e.OfferId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Product).WithMany(p => p.Offers).HasConstraintName("FK_Offer_ToProduct");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__2D10D16A4A94341F");

            entity.HasOne(d => d.Store).WithMany(p => p.Products).HasConstraintName("FK_Product_ToStore");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Store__1EA71613F8583CF0");

            entity.HasOne(d => d.Admin).WithMany(p => p.Stores).HasConstraintName("FK_Store_ToAdmin");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
