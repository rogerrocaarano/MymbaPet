using System;
using System.ComponentModel.DataAnnotations.Schema;
using c18_98_m_csharp.Models.Identity;

namespace c18_98_m_csharp.Models.Pets;

public class PetAccessAuthorization
{
    [ForeignKey("Pet")] public Guid PetId { get; set; }
    public Pet Pet { get; set; }
    [ForeignKey("User")] public Guid UserId { get; set; }
    public AppUser User { get; set; }
    [ForeignKey("Role")] public Guid RoleId { get; set; }
    public AppRole Role { get; set; }
}