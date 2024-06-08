using c18_98_m_csharp.Data;
using c18_98_m_csharp.Services.Pets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace c18_98_m_csharp.Controllers
{
    [Authorize(Roles = "Veterinarian")]
    public class PatientsController(
        ApplicationDbContext context,
        UserManager<AppUser> userManager,
        PatientsManager patientsManager
    ) : Controller
    {
        // GET: Patients
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                // redirect to Area Identity Account Login
                return RedirectToPage("Identity/Account/Login");
            }

            var patients = await patientsManager.GetAllowedPets(user);
            return View(patients);
        }
    }
}