using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace EncuestasAPIU3_4.Models.Entities;

public partial class EncuestaContext : DbContext
{
    public EncuestaContext()
    {
    }

    public EncuestaContext(DbContextOptions<EncuestaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AplicacionEncuesta> AplicacionEncuesta { get; set; }

    public virtual DbSet<Encuestas> Encuestas { get; set; }

    public virtual DbSet<Entrevistados> Entrevistados { get; set; }

    public virtual DbSet<Preguntas> Preguntas { get; set; }

    public virtual DbSet<Repuestas> Repuestas { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=vps.kolibsoftware.com;database=encuesta;user=encuesta;password=encuesta2025@D", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.10-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<AplicacionEncuesta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.IdEncuesta, "fkencuestaId_idx");

            entity.HasIndex(e => e.IdUsuario, "fkencuestaUsuario_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.FechaAplicacion).HasColumnType("datetime");
            entity.Property(e => e.IdEncuesta).HasColumnType("int(11)");
            entity.Property(e => e.IdUsuario).HasColumnType("int(11)");

            entity.HasOne(d => d.IdEncuestaNavigation).WithMany(p => p.AplicacionEncuesta)
                .HasForeignKey(d => d.IdEncuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkencuestaId");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.AplicacionEncuesta)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkencuestaUsuario");
        });

        modelBuilder.Entity<Encuestas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("encuestas");

            entity.HasIndex(e => e.IdUsuario, "fkIduser_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuario).HasColumnType("int(11)");
            entity.Property(e => e.Titulo).HasMaxLength(255);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Encuestas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkIduser");
        });

        modelBuilder.Entity<Entrevistados>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("entrevistados");

            entity.HasIndex(e => e.IdAplicacion, "fkIdAplicacion_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdAplicacion).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.NumControl).HasMaxLength(10);

            entity.HasOne(d => d.IdAplicacionNavigation).WithMany(p => p.Entrevistados)
                .HasForeignKey(d => d.IdAplicacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkIdAplicacion");
        });

        modelBuilder.Entity<Preguntas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("preguntas");

            entity.HasIndex(e => e.IdEncuesta, "fkIdEncuesta_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdEncuesta).HasColumnType("int(11)");
            entity.Property(e => e.Orden).HasColumnType("int(11)");
            entity.Property(e => e.Pregunta).HasMaxLength(255);

            entity.HasOne(d => d.IdEncuestaNavigation).WithMany(p => p.Preguntas)
                .HasForeignKey(d => d.IdEncuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkIdEncuesta");
        });

        modelBuilder.Entity<Repuestas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("repuestas");

            entity.HasIndex(e => e.IdEntrevistado, "fkentrevistadoid_idx");

            entity.HasIndex(e => e.IdPregunta, "fkpreguntaid_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdEntrevistado).HasColumnType("int(11)");
            entity.Property(e => e.IdPregunta).HasColumnType("int(11)");
            entity.Property(e => e.Valor).HasColumnType("int(11)");

            entity.HasOne(d => d.IdEntrevistadoNavigation).WithMany(p => p.Repuestas)
                .HasForeignKey(d => d.IdEntrevistado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkentrevistadoid");

            entity.HasOne(d => d.IdPreguntaNavigation).WithMany(p => p.Repuestas)
                .HasForeignKey(d => d.IdPregunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkpreguntaid");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Contrasena).HasMaxLength(20);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.NombreUsuario).HasMaxLength(45);
            entity.Property(e => e.Rol).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
