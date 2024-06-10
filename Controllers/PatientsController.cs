using c18_98_m_csharp.Data;
using c18_98_m_csharp.Services.Pets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace c18_98_m_csharp.Controllers;

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

    // GET: Patients/AddPatientBySharedCode
    public IActionResult AddPatientBySharedCode()
    {
        return View();
    }

    // POST: Patients/AddPatientBySharedCode
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddPatientBySharedCode([Bind("Code")] string code)
    {
        var accessCode = patientsManager.GetAccessCode(code);
        try
        {
            var user = await userManager.GetUserAsync(User);
            await patientsManager.AllowAccess(accessCode, user);
        }
        catch (Exception e)
        {
            if (e.GetType() == typeof(ArgumentException))
            {
                ModelState.AddModelError("Code", e.Message);
                return View();
            }

            Console.WriteLine(e);
            throw;
        }

        return RedirectToAction(nameof(Index));
    }
}