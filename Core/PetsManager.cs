﻿using System;
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
    ClinicalHistoryManager clinicalHistoryManager,
    AppDbContext context)
{
    public async Task<Pet> Register(Pet pet, AppUser user)
    {
        // Create new Clinical History
        var history = await clinicalHistoryManager.NewClinicalHistory();
        pet.ClinicalHistory = history;
        
        // Add new pet to the database
        await context.Pets.AddAsync(pet);
        await context.SaveChangesAsync();
        
        // Register pet to the user
        var tutorRole = await context.Roles.Where(role => role.Name == "PetTutor").FirstOrDefaultAsync();
        await AuthorizeAccessToPet(pet, user, tutorRole);
        
        // If the user is a veterinarian, authorize access to the pet
        var vetRole = await context.Roles.Where(role => role.Name == "Veterinarian").FirstOrDefaultAsync();
        if (await userManager.IsInRoleAsync(user, vetRole.Name))
        {
            await AuthorizeAccessToPet(pet, user, vetRole);
        }

        return await context.Pets.FindAsync(pet.Id);
    }

    public async Task AuthorizeAccessToPet(Pet pet, AppUser user, AppRole role)
    {
        var authorization = new PetAccessAuthorization
        {
            Id = Guid.NewGuid(),
            PetId = pet.Id,
            UserId = user.Id,
            RoleId = role.Id
        };
        await context.AddAsync(authorization);
        await context.SaveChangesAsync();
    }

    public async Task<List<Pet>> GetPets(AppUser user, AppRole role)
    {
        return await context.PetAccessAuthorizations
            .Where(x => x.User == user && x.Role == role)
            .Select(x => x.Pet)
            .ToListAsync();
    }
    
    // public Pet? GetPet(AppUser user, Guid petId)
    // {
    //     var role = context.Roles.FirstOrDefault(role => role.Name == "PetTutor");
    //     var userPets = GetPets(user, role).Result;
    //     return userPets.Find(pet => pet.Id == petId);
    // }

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

    public async Task<bool> UseAccessCode(PetAccessCode code, AppUser user, AppRole role)
    {
        if (code.UsedBy == null
            && code.Expiration > DateTime.Now.ToUniversalTime())
        {
            var pet = await context.Pets.FindAsync(code.PetId);
            await AuthorizeAccessToPet(pet, user, role);
            code.UsedBy = user;
            context.PetAccessCodes.Update(code);
            await context.SaveChangesAsync();
            return true;
        }

        return false;
    }
    
    public async Task Update(Pet pet)
    {
        context.Pets.Update(pet);
        await context.SaveChangesAsync();
    }
}