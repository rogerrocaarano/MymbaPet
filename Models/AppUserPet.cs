using System.ComponentModel.DataAnnotations.Schema;
using c18_98_m_csharp.Models;

public class AppUserPet
{
    [ForeignKey("AppUser")]
    public Guid TutorId { get; set; }
    public AppUser AppUser { get; set; }

    [ForeignKey("Pet")]
    public Guid PetId { get; set; }
    public Pet Pet { get; set; }
}