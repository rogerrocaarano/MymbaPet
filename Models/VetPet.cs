using System.ComponentModel.DataAnnotations.Schema;

namespace c18_98_m_csharp.Models;

public class VetPet
{
    [ForeignKey("AppUser")]
    public Guid VetId { get; set; }
    public AppUser Vet { get; set; }

    [ForeignKey("Pet")]
    public Guid PetId { get; set; }
    public Pet Pet { get; set; }
}