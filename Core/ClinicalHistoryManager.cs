﻿using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models.ClinicalHistories;
using c18_98_m_csharp.Models.Identity;
using Microsoft.AspNetCore.Identity;

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
}