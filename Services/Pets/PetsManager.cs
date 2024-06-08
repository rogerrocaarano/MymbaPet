using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models;
using Microsoft.EntityFrameworkCore;

namespace c18_98_m_csharp.Services.Pets;

public class PetsManager(
    ApplicationDbContext context)
    : ICrudService
{
    // public async Task<List<Pet>> GetPets(AppUser user)
    // {
    //     return await context.TutorPets
    //         .Where(x => x.TutorId == user.Id)
    //         .Select(x => x.Pet)
    //         .ToListAsync();
    // }
    //
    // public Task<List<Pet>> GetPatients(AppUser vet)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public bool UserIsPetTutor(AppUser user, Guid petId)
    // {
    //     var userPets = GetPets(user).Result;
    //     return userPets.Any(x => x.Id == petId);
    // }
    //
    // public bool PetExists(Guid id)
    // {
    //     return context.Pets.Any(e => e.Id == id);
    // }
    //
    // public async Task RegisterPet(Pet pet, AppUser user)
    // {
    //     pet.Id = Guid.NewGuid();
    //     context.Add(pet);
    //     await context.SaveChangesAsync();
    //     await CreateClinicalHistory(pet);
    //     await AssignPetToTutor(pet, user);
    // }
    //
    // public async Task CreateClinicalHistory(Pet pet)
    // {
    //     var clinicalHistory = new ClinicalHistory
    //     {
    //         Id = Guid.NewGuid(),
    //         PetId = pet.Id
    //     };
    //     context.Add(clinicalHistory);
    //     await context.SaveChangesAsync();
    // }
    //
    // public async Task AssignPetToTutor(Pet pet, AppUser tutor)
    // {
    //     var tutorPet = new TutorPet
    //     {
    //         PetId = pet.Id,
    //         TutorId = tutor.Id
    //     };
    //     context.Add(tutorPet);
    //     await context.SaveChangesAsync();
    // }
    //
    // public Task ChangePetTutor(Pet pet, AppUser tutor)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task AuthorizeVeterinarian(Pet pet, AppUser vet)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task DeAuthorizeVeterinarian(Pet pet, AppUser vet)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<T> Create<T>(T entity) where T : class
    {
        var pet = entity as Pet;
        pet.Id = Guid.NewGuid();
        await context.Pets.AddAsync(pet);
        await context.SaveChangesAsync();
        return pet as T;
    }

    public async Task Delete<T>(Guid id) where T : class
    {
        var pet = await context.Pets.FirstOrDefaultAsync(x => x.Id == id);
        context.Pets.Remove(pet);
        await context.SaveChangesAsync();
    }

    public async Task<T> Get<T>(Guid id) where T : class
    {
        var pet = await context.Pets.FirstOrDefaultAsync(x => x.Id == id);
        return pet as T;
    }

    public async Task<List<T>> GetAll<T>() where T : class
    {
        throw new NotImplementedException();
    }

    public async Task Update<T>(T entity) where T : class
    {
        var pet = entity as Pet;
        context.Pets.Update(pet);
        await context.SaveChangesAsync();
    }
}