using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models;
using Microsoft.EntityFrameworkCore;

namespace c18_98_m_csharp.Services.MedicalHistory;

public class ClinicalHistoryManager(ApplicationDbContext context) : ICrudService
{
    public async Task<T> Create<T>(T entity) where T : class
    {
        var clinicalHistory = entity as ClinicalHistory;
        clinicalHistory.Id = Guid.NewGuid();
        await context.ClinicalHistories.AddAsync(clinicalHistory);
        await context.SaveChangesAsync();
        return clinicalHistory as T;
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
}