using System;
using c18_98_m_csharp.Models.ClinicalHistories;
using c18_98_m_csharp.Models;
using c18_98_m_csharp.Models.Identity;
using c18_98_m_csharp.Models.Pets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace c18_98_m_csharp.Data;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public DbSet<ClinicalHistory> ClinicalHistories { get; set; }
    public DbSet<ClinicalHistoryEntry> ClinicalHistoryEntries { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<PetAccessAuthorization> PetAccessAuthorizations { get; set; }
    public DbSet<PetAccessCode> PetAccessCodes { get; set; }
}