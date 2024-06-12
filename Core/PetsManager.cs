using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models.ClinicalHistories;
using c18_98_m_csharp.Models.Identity;
using c18_98_m_csharp.Models.Pets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using shortid;
using shortid.Configuration;

namespace c18_98_m_csharp.Core;

public class PetsManager(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    AppDbContext context)
{
    public async Task<Pet> Register(Pet pet, AppUser user)
    {
        // Create new Clinical History
        var history = await NewClinicalHistory();
        pet.ClinicalHistory = history;
        // Add new pet to the database
        await context.Pets.AddAsync(pet);
        await context.SaveChangesAsync();
        // Register pet to the user
        await AuthorizeAccessToPet(pet, user, null);
        var vetRole = await context.Roles.Where(role => role.Name == "Veterinarian").FirstOrDefaultAsync();
        if (await userManager.IsInRoleAsync(user, vetRole.Name))
        {
            await AuthorizeAccessToPet(pet, user, vetRole);
        }

        return await context.Pets.FindAsync(pet.Id);
    }

    private async Task<ClinicalHistory> NewClinicalHistory()
    {
        var currentTime = DateTime.Now.ToUniversalTime();
        var history = new ClinicalHistory
        {
            Id = Guid.NewGuid(),
            CreatedAt = currentTime,
            LastUpdated = currentTime
        };
        await context.ClinicalHistories.AddAsync(history);
        await context.SaveChangesAsync();
        return await context.ClinicalHistories.FindAsync(history.Id);
    }

    public async Task AuthorizeAccessToPet(Pet pet, AppUser user, AppRole? role)
    {
        var authorization = new PetAccessAuthorization
        {
            Id = Guid.NewGuid(),
            Pet = pet,
            User = user,
            Role = role
        };
        await context.AddAsync(authorization);
        await context.SaveChangesAsync();
    }

    public async Task<List<Pet>> GetPets(AppUser user, AppRole? role)
    {
        return await context.PetAccessAuthorizations
            .Where(x => x.User == user && x.Role == role)
            .Select(x => x.Pet)
            .ToListAsync();
    }
    
    public Pet? GetPet(AppUser user, Guid petId)
    {
        var userPets = GetPets(user, null).Result;
        return userPets.Find(pet => pet.Id == petId);
    }

    public async Task<PetAccessCode> GenerateAccessCode(Pet pet)
    {
        var accessCode = new PetAccessCode
        {
            Id = Guid.NewGuid(),
            Code = ShortId.Generate(new GenerationOptions(useNumbers: true, useSpecialCharacters: false, length: 8)),
            Expiration = DateTime.Now.ToUniversalTime().AddDays(1),
            PetId = pet.Id
        };
        context.PetAccessCodes.Add(accessCode);
        await context.SaveChangesAsync();
        return accessCode;
    }

    public async Task<PetAccessCode?> GetAccessCode(string code)
    {
        return await context.PetAccessCodes
            .Where(x => x.Code == code)
            .FirstOrDefaultAsync();
    }

    public async Task UseAccessCode(PetAccessCode code, AppUser user, AppRole role)
    {
        if (code.UsedBy == null
            && code.Expiration > DateTime.Now.ToUniversalTime())
        {
            await AuthorizeAccessToPet(code.Pet, user, role);
        }
    }
    
    public async Task Update(Pet pet)
    {
        context.Pets.Update(pet);
        await context.SaveChangesAsync();
    }
}