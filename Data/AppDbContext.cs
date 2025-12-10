using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimPim.Api.Models;

namespace SimPim.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // 🔹 Tabele principale
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Investigatie> Investigatii => Set<Investigatie>();
    public DbSet<ParametruInvestigatie> ParametriInvestigatii => Set<ParametruInvestigatie>();
    public DbSet<ComandaInvestigatie> ComenziInvestigatii => Set<ComandaInvestigatie>();
    public DbSet<RezultatInvestigatie> RezultateInvestigatii => Set<RezultatInvestigatie>();

    // 🔹 Wrapper explicit pentru SaveChanges
    // (ca să fie clar pentru compilator că există metoda)
    public new int SaveChanges()
    {
        return base.SaveChanges();
    }

    // 🔹 Wrapper explicit pentru SaveChangesAsync
    public new Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 🔸 IDNP unic pentru pacienți
        modelBuilder.Entity<Patient>()
            .HasIndex(p => p.IDNP)
            .IsUnique();

        // 🔸 Relația Investigatie -> Parametri
        modelBuilder.Entity<Investigatie>()
            .HasMany(i => i.Parametri)
            .WithOne()
            .HasForeignKey(p => p.InvestigatieId)
            .OnDelete(DeleteBehavior.Cascade);

        // 🔸 Relația Comanda -> Rezultate
        modelBuilder.Entity<ComandaInvestigatie>()
            .HasMany(c => c.Rezultate)
            .WithOne()
            .HasForeignKey(r => r.ComandaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
