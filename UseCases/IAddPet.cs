using c18_98_m_csharp.Models;

namespace c18_98_m_csharp.UseCases;

public interface IAddPet
{
    bool PetExists(Guid id);
    Task RegisterPet(Pet pet, AppUser tutor);
    Task CreateClinicalHistory(Pet pet);
}