using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models;
using Microsoft.EntityFrameworkCore;

namespace c18_98_m_csharp.Services.Pets;

public class PatientsManager(ApplicationDbContext context) : PetsManager(context), IPetsAccess
{
    public async Task AllowAccess(Pet pet, AppUser user)
    {
        await context.Patients.AddAsync(new Patient
        {
            PetId = pet.Id,
            VetId = user.Id
        });
        await context.SaveChangesAsync();
    }
    
    public async Task AllowAccess(PetAccessCode accessCode, AppUser user)
    {
        var pet = Get(accessCode);
        if (pet == null)
        {
            throw new ArgumentException("Invalid access code");
        }
        await AllowAccess(pet, user);
        context.PetAccessCodes.Remove(accessCode);
        await context.SaveChangesAsync();
    }

    public async Task RevokeAccess(Pet pet, AppUser user)
    {
        var patient = await context.Patients.FindAsync(pet.Id, user.Id);
        context.Patients.Remove(patient);
        await context.SaveChangesAsync();
    }

    public async Task Transfer(Pet pet, AppUser user)
    {
        throw new NotImplementedException();
    }

    public bool HasAccess(Guid petId, AppUser user)
    {
        var hasAccessTo = GetAllowedPets(user).Result;
        return hasAccessTo.Any(x => x.Id == petId);
    }
    
    public async Task<List<Pet>> GetAllowedPets(AppUser user)
    {
        return await context.Patients
            .Where(x => x.VetId == user.Id)
            .Select(x => x.Pet)
            .ToListAsync();
    }
    
    public Pet? Get(PetAccessCode accessCode)
    {
        return context.PetAccessCodes
            .Where(x => x.Code == accessCode.Code)
            .Select(x => x.Pet)
            .FirstOrDefault();
    }
}