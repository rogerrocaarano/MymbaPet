using c18_98_m_csharp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace c18_98_m_csharp.Services.DbSeeder;

public class DbSeeder(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : IDbSeeder
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly RoleManager<AppRole> _roleManager = roleManager;

    public async Task SeedRoles()
    {
        foreach (var role in Enum.GetNames(typeof(Roles)))
        {
            if (!await VerifyRole(role))
            {
                await CreateRole(role);
            }
        }
    }

    public async Task MigrateDatabase()
    {
        await _context.Database.MigrateAsync();
    }

    private async Task<bool> VerifyRole(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    private async Task CreateRole(string roleName)
    {
        var role = new AppRole
        {
            Name = roleName
        };
        await _roleManager.CreateAsync(role);
        Console.WriteLine($"Role {roleName} created");
    }
}