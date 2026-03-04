using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Royal_Games.Domains;

namespace Royal_Games.Contexts;

public partial class Royal_GamesContext : DbContext
{
    public Royal_GamesContext()
    {
    }

    public Royal_GamesContext(DbContextOptions<Royal_GamesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClassificacaoIndicativa> ClassificacaoIndicativas { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Jogo> Jogos { get; set; }

    public virtual DbSet<Log_AlteracaoJogo> Log_AlteracaoJogos { get; set; }

    public virtual DbSet<Plataforma> Plataformas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Royal_Games;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassificacaoIndicativa>(entity =>
        {
            entity.HasKey(e => e.ClassificacaoIndicativaID).HasName("PK__Classifi__892DEC6FFCCC3DA3");

            entity.ToTable("ClassificacaoIndicativa");

            entity.Property(e => e.Classificacao)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.GeneroID).HasName("PK__Genero__A99D026808D1FFAC");

            entity.ToTable("Genero");

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Jogo>(entity =>
        {
            entity.HasKey(e => e.JogoID).HasName("PK__Jogo__591968551B5B7E8D");

            entity.ToTable("Jogo", tb =>
                {
                    tb.HasTrigger("trg_AlteracaoJogo");
                    tb.HasTrigger("trg_ExclusaoJogo");
                });

            entity.HasIndex(e => e.Nome, "UQ__Jogo__7D8FE3B2C15C8749").IsUnique();

            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StatusJogo).HasDefaultValue(true);

            entity.HasOne(d => d.ClassificacaoIndicativa).WithMany(p => p.Jogos)
                .HasForeignKey(d => d.ClassificacaoIndicativaID)
                .HasConstraintName("FK__Jogo__Classifica__534D60F1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Jogos)
                .HasForeignKey(d => d.UsuarioID)
                .HasConstraintName("FK__Jogo__UsuarioID__52593CB8");

            entity.HasMany(d => d.Generos).WithMany(p => p.Jogos)
                .UsingEntity<Dictionary<string, object>>(
                    "JogoGenero",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("GeneroID")
                        .HasConstraintName("FK_JogoGenero_Genero"),
                    l => l.HasOne<Jogo>().WithMany()
                        .HasForeignKey("JogoID")
                        .HasConstraintName("FK_JogoGenero_Jogo"),
                    j =>
                    {
                        j.HasKey("JogoID", "GeneroID");
                        j.ToTable("JogoGenero");
                    });
        });

        modelBuilder.Entity<Log_AlteracaoJogo>(entity =>
        {
            entity.HasKey(e => e.Log_AlteracaoJogoID).HasName("PK__Log_Alte__BB9D2C4F9563F44A");

            entity.ToTable("Log_AlteracaoJogo");

            entity.Property(e => e.DataAlteracao).HasPrecision(0);
            entity.Property(e => e.NomeAnterior)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrecoAnterior).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Jogo).WithMany(p => p.Log_AlteracaoJogos)
                .HasForeignKey(d => d.JogoID)
                .HasConstraintName("FK__Log_Alter__JogoI__619B8048");
        });

        modelBuilder.Entity<Plataforma>(entity =>
        {
            entity.HasKey(e => e.PlataformaID).HasName("PK__Platafor__B835678D36B379C3");

            entity.ToTable("Plataforma");

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.Jogos).WithMany(p => p.Plataformas)
                .UsingEntity<Dictionary<string, object>>(
                    "JogoPlataforma",
                    r => r.HasOne<Jogo>().WithMany()
                        .HasForeignKey("JogoID")
                        .HasConstraintName("PK_JogoPlataforma_Jogo"),
                    l => l.HasOne<Plataforma>().WithMany()
                        .HasForeignKey("PlataformaID")
                        .HasConstraintName("PK_JogoPlataforma_Plataforma"),
                    j =>
                    {
                        j.HasKey("PlataformaID", "JogoID");
                        j.ToTable("JogoPlataforma");
                    });
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioID).HasName("PK__Usuario__2B3DE798429610E5");

            entity.ToTable("Usuario", tb => tb.HasTrigger("trg_ExclusaoUsuario"));

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D1053475939B9B").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
            entity.Property(e => e.StatusUsuario).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
