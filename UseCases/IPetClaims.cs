using c18_98_m_csharp.Models;

namespace c18_98_m_csharp.UseCases;

public interface IPetClaims
{
    Task AssignPetToTutor(Pet pet, AppUser tutor);
    Task ChangePetTutor(Pet pet, AppUser tutor);
}