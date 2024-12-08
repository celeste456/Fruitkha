using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities.Entities;

public partial class FruitkhaContext : DbContext
{
    public FruitkhaContext()
    {
    }

    public FruitkhaContext(DbContextOptions<FruitkhaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<DiscountCode> DiscountCodes { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
	public virtual DbSet<ShoppingCart> ShoppingCartItem { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer("Server=.;Database=Fruitkha;Integrated Security=True;Trusted_Connection=True; TrustServerCertificate=True;");
		}
		base.OnConfiguring(optionsBuilder);
	}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07BD80EB7E");

            entity.HasIndex(e => e.Name, "UQ__Categori__737584F622ACFC7C").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comments__3214EC070DB1B69D");

            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.News).WithMany(p => p.Comments)
                .HasForeignKey(d => d.NewsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__NewsId__44FF419A");
        });

        modelBuilder.Entity<DiscountCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Discount__3214EC07E9389D25");

            entity.HasIndex(e => e.Code, "UQ__Discount__A25C5AA7CE7A5210").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.Photo).HasMaxLength(255);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__News__3214EC07C42375DE");

            entity.Property(e => e.Photo).HasMaxLength(255);
            entity.Property(e => e.PublishDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC076D8D0C5A");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.DiscountCode).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DiscountCodeId)
                .HasConstraintName("FK__Orders__Discount__4D94879B");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC079B9C4EFB");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Photo).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Catego__3A81B327");
        });

		modelBuilder.Entity<ShoppingCart>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK_ShoppingCart");

			entity.ToTable("ShoppingCart");

			entity.Property(e => e.UserId).HasMaxLength(450);

			entity.HasMany(e => e.Items)
				.WithOne(e => e.ShoppingCart)
				.HasForeignKey(e => e.ShoppingCartId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_ShoppingCart_ShoppingCartItems");
		});

		modelBuilder.Entity<ShoppingCartItem>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK_ShoppingCartItem");

			entity.ToTable("ShoppingCartItem");

			entity.Property(e => e.Quantity).HasColumnType("decimal(10, 2)");

			entity.HasOne(e => e.Product)
				.WithMany()
				.HasForeignKey(e => e.ProductId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_ShoppingCartItem_Product");
		});

		OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
