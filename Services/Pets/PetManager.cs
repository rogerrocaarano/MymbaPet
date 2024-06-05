using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models;
using Microsoft.EntityFrameworkCore;
using c18_98_m_csharp.UseCases;
using Microsoft.AspNetCore.Identity;

namespace c18_98_m_csharp.Services.Pets;

public class PetManager(
    ApplicationDbContext context)
    : IListPets, IPetClaims
{
    public async Task<List<Pet>> GetPets(AppUser user)
    {
        return await context.TutorPets
            .Where(x => x.TutorId == user.Id)
            .Select(x => x.Pet)
            .ToListAsync();
    }

    public Task<List<Pet>> GetPatients(AppUser vet)
    {
        throw new NotImplementedException();
    }

    public bool UserIsPetTutor(AppUser user, Guid petId)
    {
        var userPets = GetPets(user).Result;
        return userPets.Any(x => x.Id == petId);
    }

    public bool PetExists(Guid id)
    {
        return context.Pets.Any(e => e.Id == id);
    }

    public async Task AssignPetToTutor(Pet pet, AppUser tutor)
    {
        var tutorPet = new TutorPet
        {
            PetId = pet.Id,
            TutorId = tutor.Id
        };
        context.Add(tutorPet);
        await context.SaveChangesAsync();
    }

    public Task ChangePetTutor(Pet pet, AppUser tutor)
    {
        throw new NotImplementedException();
    }
}