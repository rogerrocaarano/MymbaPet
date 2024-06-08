using c18_98_m_csharp.Models;

namespace c18_98_m_csharp.Services.MedicalHistory;

public interface IClinicalEntries
{
    Task<ClinicalHistoryEntry> AddEntry(ClinicalHistoryEntry entry);
    Task<ClinicalHistoryEntry> GetEntry(Guid id);
    Task<List<ClinicalHistoryEntry>> GetAllEntries(ClinicalHistory history);
}