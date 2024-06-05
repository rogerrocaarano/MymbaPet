using System.ComponentModel.DataAnnotations.Schema;

namespace c18_98_m_csharp.Models;

public class ClinicalHistoryEntry
{
    public Guid Id { get; set; }
    [ForeignKey("ClinicalHistory")] public Guid ClinicalHistoryId { get; set; }
    public ClinicalHistory ClinicalHistory { get; set; }
    private DateTime _dateTime;

    public DateTime DateTime
    {
        get { return _dateTime; }
        set { _dateTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
    }
}