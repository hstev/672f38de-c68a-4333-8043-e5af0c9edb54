using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SalesVehicleItmApi.Models;

public partial class VehicleSalesDbContext : DbContext
{
    public VehicleSalesDbContext()
    {
    }

    public VehicleSalesDbContext(DbContextOptions<VehicleSalesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agency> Agencies { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=IDEAPAD3\\SQLEXPRESS; Database=VehicleSalesDB; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Agency__3214EC07EFEA1895");

            entity.ToTable("Agency");

            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brand__3214EC0773515764");

            entity.ToTable("Brand");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC078B934F9D");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D105340C8CB890").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sale__3214EC07C9CA65CD");

            entity.ToTable("Sale");

            entity.HasIndex(e => e.VehicleId, "UQ__Sale__476B54936040504C").IsUnique();

            entity.Property(e => e.SaleDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Agency).WithMany(p => p.Sales)
                .HasForeignKey(d => d.AgencyId)
                .HasConstraintName("FK__Sale__AgencyId__4316F928");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Sale__CustomerId__440B1D61");

            entity.HasOne(d => d.Vehicle).WithOne(p => p.Sale)
                .HasForeignKey<Sale>(d => d.VehicleId)
                .HasConstraintName("FK__Sale__VehicleId__44FF419A");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehicle__3214EC0744DF26AE");

            entity.ToTable("Vehicle");

            entity.Property(e => e.Engine).HasMaxLength(255);
            entity.Property(e => e.FuelType).HasMaxLength(100);

            entity.HasOne(d => d.Brand).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__Vehicle__BrandId__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
