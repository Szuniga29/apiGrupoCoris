using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace apiGrupoCoris.Models;

public partial class apiGrupoCorisDbContext : DbContext
{
    public apiGrupoCorisDbContext()
    {
    }

    public apiGrupoCorisDbContext(DbContextOptions<apiGrupoCorisDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Venta>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Canal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cliente)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Detalle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngresa).HasColumnType("date");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrecioxGr).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Presentacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubCategoria)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
