using System.ComponentModel.DataAnnotations.Schema;

namespace c18_98_m_csharp.Models;

public class PetAccessCode
{
    public Guid Id { get; set; }
    public string Code { get; set; }

    public DateTime Expiration
    {
        get => _expiration;
        set => _expiration = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
    private DateTime _expiration;
    [ForeignKey("Pet")] public Guid PetId { get; set; }
    public Pet Pet { get; set; }
    public bool Used { get; set; }
}