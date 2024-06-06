using System.ComponentModel.DataAnnotations.Schema;

namespace c18_98_m_csharp.Models;

public class TutorPet
{
    [ForeignKey("AppUser")]
    public Guid TutorId { get; set; }
    public AppUser AppUser { get; set; }

    [ForeignKey("Pet")]
    public Guid PetId { get; set; }
    public Pet Pet { get; set; }
}