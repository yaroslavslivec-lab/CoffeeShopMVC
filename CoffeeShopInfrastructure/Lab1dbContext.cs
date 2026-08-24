using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CoffeeShopDomain.Model;

namespace CoffeeShopInfrastructure;



public partial class Lab1dbContext : DbContext
{
    public Lab1dbContext()
    {
    }

    public Lab1dbContext(DbContextOptions<Lab1dbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Itemsize> Itemsizes { get; set; }

    public virtual DbSet<Itemvariation> Itemvariations { get; set; }

    public virtual DbSet<Menuitem> Menuitems { get; set; }

    public virtual DbSet<Nutritionalvalue> Nutritionalvalues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=lab1db;Username=yaroslav;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ingredients_pkey");

            entity.ToTable("ingredients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IngredientName)
                .HasMaxLength(100)
                .HasColumnName("ingredient_name");
        });

        modelBuilder.Entity<Itemsize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("itemsizes_pkey");

            entity.ToTable("itemsizes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SizeName)
                .HasMaxLength(50)
                .HasColumnName("size_name");
        });

        modelBuilder.Entity<Itemvariation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("itemvariations_pkey");

            entity.ToTable("itemvariations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MenuItemId).HasColumnName("menu_item_id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.SizeId).HasColumnName("size_id");

            entity.HasOne(d => d.MenuItem).WithMany(p => p.Itemvariations)
                .HasForeignKey(d => d.MenuItemId)
                .HasConstraintName("fk_variations_menuitems");

            entity.HasOne(d => d.Size).WithMany(p => p.Itemvariations)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("fk_variations_sizes");
        });

        modelBuilder.Entity<Menuitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("menuitems_pkey");

            entity.ToTable("menuitems");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.ItemName)
                .HasMaxLength(255)
                .HasColumnName("item_name");

            entity.HasOne(d => d.Category).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("fk_menuitems_categories");

            entity.HasMany(d => d.Ingredients).WithMany(p => p.MenuItems)
                .UsingEntity<Dictionary<string, object>>(
                    "Itemrecipe",
                    r => r.HasOne<Ingredient>().WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_recipes_ingredients"),
                    l => l.HasOne<Menuitem>().WithMany()
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_recipes_menuitems"),
                    j =>
                    {
                        j.HasKey("MenuItemId", "IngredientId").HasName("itemrecipes_pkey");
                        j.ToTable("itemrecipes");
                        j.IndexerProperty<int>("MenuItemId").HasColumnName("menu_item_id");
                        j.IndexerProperty<int>("IngredientId").HasColumnName("ingredient_id");
                    });
        });

        modelBuilder.Entity<Nutritionalvalue>(entity =>
        {
            entity.HasKey(e => e.MenuItemId).HasName("nutritionalvalue_pkey");

            entity.ToTable("nutritionalvalue");

            entity.Property(e => e.MenuItemId)
                .ValueGeneratedNever()
                .HasColumnName("menu_item_id");
            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.Carbs)
                .HasPrecision(5, 2)
                .HasColumnName("carbs");
            entity.Property(e => e.Fats)
                .HasPrecision(5, 2)
                .HasColumnName("fats");
            entity.Property(e => e.Proteins)
                .HasPrecision(5, 2)
                .HasColumnName("proteins");

            entity.HasOne(d => d.MenuItem).WithOne(p => p.Nutritionalvalue)
                .HasForeignKey<Nutritionalvalue>(d => d.MenuItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_nutritional_menuitems");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
