using c18_98_m_csharp.Models;

namespace c18_98_m_csharp.UseCases;

public interface IListPets
{
    Task<List<Pet>> GetPets(AppUser user);
    Task<List<Pet>> GetPatients(AppUser vet);
    bool UserIsPetTutor(AppUser tutor, Guid petId);
    bool PetExists(Guid id);
}