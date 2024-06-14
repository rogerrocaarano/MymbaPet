using System;
using System.Threading.Tasks;
using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace c18_98_m_csharp.Services.DbSeeder;

public class DbSeeder(
    IOptions<DbSeederSettings> dbSeederSettingsOptions,
    AppDbContext context,
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager
    ) : IDbSeeder
{
    private readonly DbSeederSettings _dbSeederSettings = dbSeederSettingsOptions.Value;
    private readonly AppDbContext _context = context;
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

    public async Task AddRoleToAdminUser()
    {
        var AdminUser = await _userManager.FindByEmailAsync(_dbSeederSettings.AdminUser);
        if (AdminUser == null)
        {
            Console.WriteLine("Admin user not found");
            return;
        }
        if (await _userManager.IsInRoleAsync(AdminUser, Roles.Admin.ToString()))
        {
            Console.WriteLine("Admin user already has Admin role");
            return;
        }
        Console.WriteLine("Adding Admin role to Admin user");
        await _userManager.AddToRoleAsync(AdminUser, Roles.Admin.ToString());
        await _userManager.ConfirmEmailAsync(AdminUser, await _userManager.GenerateEmailConfirmationTokenAsync(AdminUser));
    }
}