using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TourMgmtAPI.Models;

public partial class TourMgmtDbContext : DbContext
{
    public TourMgmtDbContext()
    {
    }

    public TourMgmtDbContext(DbContextOptions<TourMgmtDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bus> Buses { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<TripMaster> TripMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=WIN11;Database=TourMgmtDb;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bus>(entity =>
        {
            entity.HasKey(e => e.BusId).HasName("PK__Buses__6A0F60B5AA152D35");

            entity.ToTable("Buses", "Tour");

            entity.HasIndex(e => e.RegistrationNumber, "UQ__Buses__E88646023E3F7A6A").IsUnique();

            entity.Property(e => e.FuelType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.TripId).HasName("PK__Trips__51DC713E99F5E6C3");

            entity.ToTable("Trips", "Tour");

            entity.Property(e => e.CostOfTrip).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DestinationLocation)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NameOfTrip)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StartingLocation)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TripMaster>(entity =>
        {
            entity.HasKey(e => e.TripMasterId).HasName("PK__TripMast__CD01D344CC964DD0");

            entity.ToTable("TripMaster", "Tour");

            entity.Property(e => e.ConductorName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DriverName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Bus).WithMany(p => p.TripMasters)
                .HasForeignKey(d => d.BusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK__TripMaste__BusId__3B75D760");

            entity.HasOne(d => d.Trip).WithMany(p => p.TripMasters)
                .HasForeignKey(d => d.TripId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK__TripMaste__TripI__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
