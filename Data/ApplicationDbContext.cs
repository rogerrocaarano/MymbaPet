﻿using c18_98_m_csharp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace c18_98_m_csharp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<ClinicalAnalysis> ClinicalAnalyses { get; set; }
    public DbSet<ClinicalHistory> ClinicalHistories { get; set; }
    // public DbSet<ClinicalHistoryEntry> ClinicalHistoryEntries { get; set; }
    public DbSet<MedicalEvaluation> MedicalEvaluations { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<PetCenter> PetCenters { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    // public DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }
    public DbSet<Surgery> Surgeries { get; set; }
    public DbSet<Vaccination> Vaccinations { get; set; }
    public DbSet<VaccinationCard> VaccinationCards { get; set; }
    // public DbSet<VaccinationCardEntry> VaccinationCardEntries { get; set; }
    public DbSet<VetService> VetServices { get; set; }
}