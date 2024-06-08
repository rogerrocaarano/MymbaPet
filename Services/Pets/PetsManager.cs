using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models;
using Microsoft.EntityFrameworkCore;

namespace c18_98_m_csharp.Services.Pets;

public class PetsManager(
    ApplicationDbContext context)
    : ICrudService
{
    public async Task<T> Create<T>(T entity) where T : class
    {
        var pet = entity as Pet;
        pet.Id = Guid.NewGuid();
        await context.Pets.AddAsync(pet);
        await context.SaveChangesAsync();
        return pet as T;
    }

    public async Task Delete<T>(Guid id) where T : class
    {
        var pet = await context.Pets.FirstOrDefaultAsync(x => x.Id == id);
        context.Pets.Remove(pet);
        await context.SaveChangesAsync();
    }

    public async Task<T> Get<T>(Guid id) where T : class
    {
        var pet = await context.Pets.FirstOrDefaultAsync(x => x.Id == id);
        return pet as T;
    }

    public async Task<List<T>> GetAll<T>() where T : class
    {
        throw new NotImplementedException();
    }

    public async Task Update<T>(T entity) where T : class
    {
        var pet = entity as Pet;
        context.Pets.Update(pet);
        await context.SaveChangesAsync();
    }
}