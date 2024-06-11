using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models;
using Microsoft.EntityFrameworkCore;

namespace c18_98_m_csharp.Services.MedicalHistory;

public class ClinicalEntryManager(
    ApplicationDbContext context) : ICrudService
{
    public async Task<T> Create<T>(T entity) where T : class
    {
        var entry = entity as ClinicalHistoryEntry;
        entry.Id = Guid.NewGuid();
        var currentTime = DateTime.Now;
        entry.Created = currentTime;
        entry.LastUpdated = currentTime;
        entry.Status = "ACTIVE";
        await context.ClinicalHistoryEntries.AddAsync(entry);
        await context.SaveChangesAsync();
        return entry as T;
    }

    public async Task Delete<T>(Guid id) where T : class
    {
        throw new NotImplementedException();
    }

    public async Task<T> Get<T>(Guid id) where T : class
    {
        var entry = await context.ClinicalHistoryEntries
            .FirstOrDefaultAsync(x => x.Id == id);
        return entry as T;
    }

    public async Task<List<T>> GetAll<T>() where T : class
    {
        throw new NotImplementedException();
    }

    public async Task<List<T>> GetAll<T>(ClinicalHistory history) where T : class
    {
        var entries = await context.ClinicalHistoryEntries
            .Where(x => x.ClinicalHistoryId == history.Id)
            .ToListAsync();
        return entries as List<T>;
    }

    public async Task Update<T>(T entity) where T : class
    {
        var entry = entity as ClinicalHistoryEntry;
        entry.LastUpdated = DateTime.Now;
        context.ClinicalHistoryEntries.Update(entry);
        await context.SaveChangesAsync();
    }
}