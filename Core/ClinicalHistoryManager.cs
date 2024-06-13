using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models.ClinicalHistories;
using c18_98_m_csharp.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace c18_98_m_csharp.Core;

public class ClinicalHistoryManager(
    AppDbContext context,
    UserManager<AppUser> userManager)
{
    public async Task<ClinicalHistory> NewClinicalHistory()
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

    public async Task<ClinicalHistory?> GetHistory(Guid id, AppUser user)
    {
        var allowedPets = await context.PetAccessAuthorizations
            .Where(x => x.UserId == user.Id)
            .Select(x => x.Pet)
            .ToListAsync();
        if (allowedPets.Any(pet => pet.ClinicalHistoryId == id))
        {
            return await context.ClinicalHistories.FindAsync(id);
        }
        return null;
    }
    
    public async Task<List<ClinicalHistoryEntry>> GetEntries(ClinicalHistory history)
    {
        return await context.ClinicalHistoryEntries
            .Where(x => x.ClinicalHistoryId == history.Id)
            .ToListAsync();
    }
}