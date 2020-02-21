using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Programas.Models
{
    public partial class empleoproyectosContext : DbContext
    {
        public empleoproyectosContext()
        {
        }

        public empleoproyectosContext(DbContextOptions<empleoproyectosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actores> Actores { get; set; }
        public virtual DbSet<EtapaActor> EtapaActor { get; set; }
        public virtual DbSet<Etapas> Etapas { get; set; }
        public virtual DbSet<EtapasCambios> EtapasCambios { get; set; }
        public virtual DbSet<ProgramaCircuito> ProgramaCircuito { get; set; }
        public virtual DbSet<ProgramaGrupo> ProgramaGrupo { get; set; }
        public virtual DbSet<ProgramaGrupos> ProgramaGrupos { get; set; }
        public virtual DbSet<ProgramasPrestacion> ProgramasPrestacion { get; set; }
        public virtual DbSet<Resultados> Resultados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=S1-dIXX-SQL07;Initial Catalog=empleoproyectos;Integrated Security=SSPI");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actores>(entity =>
            {
                entity.HasKey(e => e.Actor);

                entity.Property(e => e.Actor)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nota)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EtapaActor>(entity =>
            {
                entity.HasKey(e => new { e.Actor, e.CambioEstado, e.GrupoPrograma });

                entity.HasIndex(e => new { e.Actor, e.GrupoPrograma })
                    .HasName("IX_EtapaActor-GrupoPrograma");

                entity.HasIndex(e => new { e.CambioEstado, e.GrupoPrograma })
                    .HasName("IX_Estado_Actores");

                entity.Property(e => e.Actor)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CambioEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nota)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.ActorNavigation)
                    .WithMany(p => p.EtapaActor)
                    .HasForeignKey(d => d.Actor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ETAPAACT_REF_447_ACTORES");

                entity.HasOne(d => d.CambioEstadoNavigation)
                    .WithMany(p => p.EtapaActor)
                    .HasForeignKey(d => d.CambioEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ETAPAACT_REF_827_ETAPASCA");

                entity.HasOne(d => d.GrupoProgramaNavigation)
                    .WithMany(p => p.EtapaActor)
                    .HasForeignKey(d => d.GrupoPrograma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ETAPAACT_REF_3616_PROGRAMA");
            });

            modelBuilder.Entity<Etapas>(entity =>
            {
                entity.HasKey(e => e.Etapa);

                entity.Property(e => e.Etapa).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EtapasCambios>(entity =>
            {
                entity.HasKey(e => e.CambioEstado);

                entity.HasIndex(e => new { e.Etapa, e.Resultado, e.Lugar })
                    .HasName("IX_EtapaResultadoLugar");

                entity.HasIndex(e => new { e.ProxEtapa, e.ProxResultado, e.Lugar })
                    .HasName("IX_ProxEtapaProxResultLugar");

                entity.Property(e => e.CambioEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PublicarRrhh).HasColumnName("PublicarRRHH");
            });

            modelBuilder.Entity<ProgramaCircuito>(entity =>
            {
                entity.HasKey(e => e.Circuito);

                entity.Property(e => e.Circuito).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Pne)
                    .HasColumnName("PNE")
                    .HasDefaultValueSql("(0)");
            });

            modelBuilder.Entity<ProgramaGrupo>(entity =>
            {
                entity.HasKey(e => e.GrupoPrograma);

                entity.Property(e => e.GrupoPrograma).ValueGeneratedNever();

                entity.Property(e => e.DatoEmpleadorId).HasColumnName("DatoEmpleadorID");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CircuitoNavigation)
                    .WithMany(p => p.ProgramaGrupo)
                    .HasForeignKey(d => d.Circuito)
                    .HasConstraintName("FK_PROGRAMA_REF_30588_PROGRAMA");
            });

            modelBuilder.Entity<ProgramaGrupos>(entity =>
            {
                entity.HasKey(e => new { e.GrupoPrograma, e.Programa });

                entity.Property(e => e.Programa).HasColumnName("PROGRAMA");

                entity.HasOne(d => d.GrupoProgramaNavigation)
                    .WithMany(p => p.ProgramaGrupos)
                    .HasForeignKey(d => d.GrupoPrograma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROGRAMA_REF_3601_PROGRAMA");
            });

            modelBuilder.Entity<ProgramasPrestacion>(entity =>
            {
                entity.HasKey(e => e.Programa);

                entity.ToTable("PROGRAMAS");

                entity.Property(e => e.Programa)
                    .HasColumnName("PROGRAMA")
                    .ValueGeneratedNever();

                entity.Property(e => e.Anio).HasColumnName("ANIO");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionMax).HasColumnName("DURACION_MAX");

                entity.Property(e => e.DuracionMin).HasColumnName("DURACION_MIN");

                entity.Property(e => e.EdadMax).HasColumnName("EDAD_MAX");

                entity.Property(e => e.EdadMin).HasColumnName("EDAD_MIN");

                entity.Property(e => e.EstadoPrograma)
                    .HasColumnName("estado_programa")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.FechaFin)
                    .HasColumnName("FECHA_FIN")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaInicio)
                    .HasColumnName("FECHA_INICIO")
                    .HasColumnType("datetime");

                entity.Property(e => e.LineaDeAccion).HasColumnName("lineaDeAccion");

                entity.Property(e => e.Sexo)
                    .HasColumnName("SEXO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TipoPrograma)
                    .HasColumnName("TIPO_PROGRAMA")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Resultados>(entity =>
            {
                entity.HasKey(e => new { e.Etapa, e.Resultado });

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EtapaNavigation)
                    .WithMany(p => p.Resultados)
                    .HasForeignKey(d => d.Etapa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RESULTAD_REF_197_ETAPAS");
            });
        }
    }
}
