namespace c18_98_m_csharp.Services.DbSeeder;


public interface IDbSeeder
{
    Task SeedRoles();
    Task MigrateDatabase();
}