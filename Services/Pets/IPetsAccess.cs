using c18_98_m_csharp.Models;

namespace c18_98_m_csharp.Services.Pets;

public interface IPetsAccess
{
    Task AllowAccess(Pet pet, AppUser user);
    Task RevokeAccess(Pet pet, AppUser user);
    Task Transfer(Pet pet, AppUser user);
    bool HasAccess(Guid petId, AppUser user);
}