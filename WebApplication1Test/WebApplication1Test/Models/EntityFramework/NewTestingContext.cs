using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace WebApplication1Test.Models.EntityFramework;

public partial class NewTestingContext : DbContext
{
    public NewTestingContext()
    {
    }

    public NewTestingContext(DbContextOptions<NewTestingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Order { get; set; }

    public virtual DbSet<Product> Product { get; set; }

    public virtual DbSet<ProductOrder> ProductOrder { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=ConnectionStrings:mysqldb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.3.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.HasKey(e => e.ProductOrderId).HasName("PRIMARY");

            entity.Property(e => e.ProductOrderId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
