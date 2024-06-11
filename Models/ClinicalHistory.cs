using System.ComponentModel.DataAnnotations.Schema;

namespace c18_98_m_csharp.Models;

public class ClinicalHistory
{
    public Guid Id { get; set; }

    public DateTime CreatedAt
    {
        get => _createdAt;
        set => _createdAt = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    private DateTime _createdAt;
    [ForeignKey("ClinicalHistoryEntry")] public Guid? LastEntryId { get; set; }
    public ClinicalHistoryEntry? LastEntry { get; set; }

    public DateTime LastUpdated
    {
        get => _lastUpdated;
        set => _lastUpdated = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    private DateTime _lastUpdated { get; set; }
}