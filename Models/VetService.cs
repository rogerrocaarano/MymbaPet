namespace c18_98_m_csharp.Models;

public class VetService
{
    public Guid Id { get; set; }
    public DateTime RequestDate { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime LastUpdated { get; set; }
    public bool IsEmergency { get; set; }
    public string? Description { get; set; }
}