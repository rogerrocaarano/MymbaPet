using System;
using System.ComponentModel.DataAnnotations.Schema;
using c18_98_m_csharp.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace c18_98_m_csharp.Models.Pets;

[PrimaryKey(nameof(Id))]
public class PetAccessAuthorization
{
    public Guid Id { get; set; }
    [ForeignKey("Pet")] public Guid PetId { get; set; }
    public Pet Pet { get; set; }
    [ForeignKey("User")] public Guid UserId { get; set; }
    public AppUser User { get; set; }
    [ForeignKey("Role")] public Guid? RoleId { get; set; }
    public AppRole? Role { get; set; }
}