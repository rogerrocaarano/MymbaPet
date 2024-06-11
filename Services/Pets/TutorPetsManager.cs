using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models;
using c18_98_m_csharp.Services.MedicalHistory;
using Microsoft.EntityFrameworkCore;
using shortid;
using shortid.Configuration;

namespace c18_98_m_csharp.Services.Pets;

public class TutorPetsManager(
    ApplicationDbContext context,
    ClinicalHistoryManager clinicalHistoryManager) : PetsManager(context), IPetsAccess
{
    public async Task AllowAccess(Pet pet, AppUser user)
    {
        await context.TutorPets.AddAsync(new TutorPet
        {
            PetId = pet.Id,
            TutorId = user.Id
        });
        await context.SaveChangesAsync();
    }

    public async Task RevokeAccess(Pet pet, AppUser user)
    {
        var tutorPet = await context.TutorPets.FindAsync(pet.Id, user.Id);
        context.TutorPets.Remove(tutorPet);
        await context.SaveChangesAsync();
    }

    public async Task Transfer(Pet pet, AppUser user)
    {
        var tutorPet = await context.TutorPets.FindAsync(pet.Id, user.Id);
        context.TutorPets.Remove(tutorPet);
        await AllowAccess(pet, user);
    }

    public bool HasAccess(Guid petId, AppUser user)
    {
        var hasAccessTo = GetAllowedPets(user).Result;
        return hasAccessTo.Any(x => x.Id == petId);
    }

    public async Task<List<Pet>> GetAllowedPets(AppUser user)
    {
        return await context.TutorPets
            .Where(x => x.TutorId == user.Id)
            .Select(x => x.Pet)
            .ToListAsync();
    }

    public async Task RegisterPet(Pet pet, AppUser user)
    {
        pet = await base.Create(pet);
        await clinicalHistoryManager.Create(pet);
        await AllowAccess(pet, user);
    }

    public async Task<PetAccessCode> GenerateAccessCode(Pet pet)
    {
        var accessCode = new PetAccessCode
        {
            Id = Guid.NewGuid(),
            Code = ShortId.Generate(new GenerationOptions(useNumbers: true, useSpecialCharacters: false, length: 8)),
            Expiration = DateTime.Now.ToUniversalTime().AddDays(1),
            PetId = pet.Id,
            Used = false
        };
        context.PetAccessCodes.Add(accessCode);
        await context.SaveChangesAsync();
        return accessCode;
    }
}