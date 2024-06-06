using System.ComponentModel.DataAnnotations.Schema;

namespace c18_98_m_csharp.Models;

public class ClinicalHistory
{
    public Guid Id { get; set; }
    [ForeignKey("Pet")] public Guid PetId { get; set; }
    public Pet Pet { get; set; }
}