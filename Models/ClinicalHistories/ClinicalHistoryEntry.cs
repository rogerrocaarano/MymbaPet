using System;
using System.ComponentModel.DataAnnotations.Schema;
using c18_98_m_csharp.Models.Identity;

namespace c18_98_m_csharp.Models.ClinicalHistories;

public class ClinicalHistoryEntry
{
    public Guid Id { get; set; }
    [ForeignKey("ClinicalHistory")] public Guid ClinicalHistoryId { get; set; }
    public ClinicalHistory ClinicalHistory { get; set; }
    private DateTime _created;

    public DateTime Created
    {
        get => _created;
        set => _created = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    private DateTime _lastUpdated;

    public DateTime LastUpdated
    {
        get => _lastUpdated;
        set => _lastUpdated = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    public string ServiceType { get; set; }
    public float PetWeight { get; set; }
    public string ConsultReason { get; set; }
    public string? Observations { get; set; }
    public string? Diagnosis { get; set; }
    public string? Treatment { get; set; }
    public string Status { get; set; } = "Pending";
    [ForeignKey("Vet")] public Guid VetId { get; set; }
    public AppUser Vet { get; set; }
}