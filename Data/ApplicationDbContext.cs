using c18_98_m_csharp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace c18_98_m_csharp.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<TutorPet>()
            .HasKey(x => new { x.TutorId, x.PetId });

        builder.Entity<Patient>()
            .HasKey(x => new { x.VetId, x.PetId });
    }

    public DbSet<ClinicalHistory> ClinicalHistories { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<TutorPet> TutorPets { get; set; }
    public DbSet<Patient> Patients { get; set; }
}