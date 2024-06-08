using System.ComponentModel.DataAnnotations.Schema;

namespace c18_98_m_csharp.Models;

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

    public string? Description { get; set; }
    private DateTime _lastUpdated;

    public DateTime LastUpdated
    {
        get => _lastUpdated;
        set => _lastUpdated = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
}