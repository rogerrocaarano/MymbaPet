using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models;
using Microsoft.EntityFrameworkCore;

namespace c18_98_m_csharp.Services.MedicalHistory;

public class ClinicalHistoryManager(
    ApplicationDbContext context,
    ClinicalEntryManager entries) : ICrudService, IClinicalEntries
{
    public async Task<T> Create<T>(T entity) where T : class
    {
        var clinicalHistory = entity as ClinicalHistory;
        await context.ClinicalHistories.AddAsync(clinicalHistory);
        await context.SaveChangesAsync();
        return clinicalHistory as T;
    }

    public async Task<ClinicalHistory> Create(Pet pet)
    {
        var currentTime = DateTime.Now;
        var clinicalHistory = new ClinicalHistory
        {
            Id = Guid.NewGuid(),
            CreatedAt = currentTime,
            LastUpdated = currentTime,
            PetId = pet.Id,
            Pet = pet
        };
        return await Create(clinicalHistory);
    }

    public async Task Delete<T>(Guid id) where T : class
    {
        var clinicalHistory = await context.ClinicalHistories.FirstOrDefaultAsync(x => x.Id == id);
        context.ClinicalHistories.Remove(clinicalHistory);
        await context.SaveChangesAsync();
    }

    public async Task<T> Get<T>(Guid id) where T : class
    {
        var clinicalHistory = await context.ClinicalHistories.FirstOrDefaultAsync(x => x.Id == id);
        return clinicalHistory as T;
    }

    public async Task<List<T>> GetAll<T>() where T : class
    {
        throw new NotImplementedException();
    }

    public async Task Update<T>(T entity) where T : class
    {
        var clinicalHistory = entity as ClinicalHistory;
        clinicalHistory.LastUpdated = DateTime.Now;
        context.ClinicalHistories.Update(clinicalHistory);
        await context.SaveChangesAsync();
    }

    public async Task<ClinicalHistoryEntry> AddEntry(ClinicalHistoryEntry entry)
    {
        return await entries.Create(entry);
    }

    public async Task<ClinicalHistoryEntry> GetEntry(Guid id)
    {
        return await entries.Get<ClinicalHistoryEntry>(id);
    }

    public async Task<List<ClinicalHistoryEntry>> GetAllEntries(ClinicalHistory history)
    {
        return await entries.GetAll<ClinicalHistoryEntry>(history);
    }
}