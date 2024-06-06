using Microsoft.AspNetCore.Mvc;
using c18_98_m_csharp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace c18_98_m_csharp.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(ApplicationDbContext context, UserManager<AppUser> userManager) : Controller
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<IActionResult> Index()
    {
        return View();
    }
}