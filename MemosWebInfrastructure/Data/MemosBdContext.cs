
using MemosWebApplicationCore.Entites;
using Microsoft.EntityFrameworkCore;

namespace MemosWebInfrastructure.Data;

public partial class MemosBdContext : DbContext
{
    public MemosBdContext()
    {
    }

    public MemosBdContext(DbContextOptions<MemosBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Compte> Comptes { get; set; }

    public virtual DbSet<Memo> Memos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=BD/MemosBD.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Compte>(entity =>
        {
            entity.ToTable("Compte");

            entity.Property(e => e.DateCreation).HasColumnType("DATETIME");
            entity.Property(e => e.DateDerniereConnexion).HasColumnType("DATETIME");

            entity.HasIndex(e => e.NomUtilisateur).IsUnique();
        });

        modelBuilder.Entity<Memo>(entity =>
        {
            entity.ToTable("Memo");

            entity.Property(e => e.DateCreation).HasColumnType("DATETIME");

            entity.HasOne(d => d.IdCompteNavigation).WithMany(p => p.Memos)
                .HasForeignKey(d => d.IdCompte)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
