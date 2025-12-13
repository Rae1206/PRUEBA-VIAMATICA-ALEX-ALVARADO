using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Modelo.Entities;

namespace Repository.Context;

public partial class CineDbContext : DbContext
{
    public CineDbContext()
    {
    }

    public CineDbContext(DbContextOptions<CineDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<PeliculaSalacine> PeliculaSalacines { get; set; }

    public virtual DbSet<SalaCine> SalaCines { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.IdPelicula);

            entity.ToTable("pelicula");

            entity.HasIndex(e => e.Nombre, "IX_pelicula_nombre");

            entity.Property(e => e.IdPelicula).HasColumnName("id_pelicula");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Duracion).HasColumnName("duracion");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<PeliculaSalacine>(entity =>
        {
            entity.HasKey(e => e.IdPeliculaSala);

            entity.ToTable("pelicula_salacine");

            entity.HasIndex(e => e.FechaPublicacion, "IX_pelicula_salacine_fecha_publicacion");

            entity.HasIndex(e => e.IdPelicula, "IX_pelicula_salacine_id_pelicula");

            entity.HasIndex(e => e.IdSalaCine, "IX_pelicula_salacine_id_sala_cine");

            entity.HasIndex(e => new { e.IdSalaCine, e.IdPelicula, e.FechaPublicacion }, "UX_pelicula_salacine_unique_activo")
                .IsUnique()
                .HasFilter("([eliminado]=(0))");

            entity.Property(e => e.IdPeliculaSala).HasColumnName("id_pelicula_sala");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaPublicacion).HasColumnName("fecha_publicacion");
            entity.Property(e => e.IdPelicula).HasColumnName("id_pelicula");
            entity.Property(e => e.IdSalaCine).HasColumnName("id_sala_cine");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.PeliculaSalacines)
                .HasForeignKey(d => d.IdPelicula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pelicula_salacine_pelicula");

            entity.HasOne(d => d.IdSalaCineNavigation).WithMany(p => p.PeliculaSalacines)
                .HasForeignKey(d => d.IdSalaCine)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pelicula_salacine_sala_cine");
        });

        modelBuilder.Entity<SalaCine>(entity =>
        {
            entity.HasKey(e => e.IdSala);

            entity.ToTable("sala_cine");

            entity.HasIndex(e => e.Nombre, "UX_sala_cine_nombre_activo")
                .IsUnique()
                .HasFilter("([eliminado]=(0))");

            entity.Property(e => e.IdSala).HasColumnName("id_sala");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(120)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UX_users_email_activo")
                .IsUnique()
                .HasFilter("([eliminado]=(0))");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("user_name");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Eliminado)
                .HasDefaultValue(false)
                .HasColumnName("eliminado");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
