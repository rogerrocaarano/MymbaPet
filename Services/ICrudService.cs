namespace c18_98_m_csharp.Services;

public interface ICrudService
{
    public Task<T> Create<T>(T entity) where T : class;
    public Task Delete<T>(Guid id) where T : class;
    public Task<T> Get<T>(Guid id) where T : class;
    public Task<List<T>> GetAll<T>() where T : class;
    public Task Update<T>(T entity) where T : class;
}