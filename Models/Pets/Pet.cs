using System;
using System.ComponentModel.DataAnnotations.Schema;
using c18_98_m_csharp.Models.ClinicalHistories;

namespace c18_98_m_csharp.Models.Pets;

public class Pet
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Breed { get; set; }
    public string? Color { get; set; }
    public string? Species { get; set; }
    private DateTime _birthdate;

    public DateTime Birthdate
    {
        get { return _birthdate; }
        set { _birthdate = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
    }

    public string? Sex { get; set; }
    public string? MicrochipId { get; set; }
    public string? Notes { get; set; }
    [ForeignKey("ClinicalHistory")] public Guid ClinicalHistoryId { get; set; }
    public ClinicalHistory? ClinicalHistory { get; set; }
}