using c18_98_m_csharp.Models;
using c18_98_m_csharp.Services.MedicalHistory;
using c18_98_m_csharp.Services.Pets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace c18_98_m_csharp.Controllers;

[Authorize]
public class ClinicalHistoriesController(
    UserManager<AppUser> userManager,
    ClinicalHistoryManager histories,
    PatientsManager patients,
    TutorPetsManager tutorPets
) : Controller
{
    // GET: ClinicalHistories/Details/id
    public IActionResult Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = userManager.GetUserAsync(User).Result;

        if (user == null)
        {
            return NotFound();
        }
        
        var history = histories.Get<ClinicalHistory>(id.Value).Result;
        
        if (history == null)
        {
            return NotFound();
        }

        var pet = histories.GetPatient(history).Result;
        if (pet == null)
        {
            return NotFound();
        }
        
        if (User.IsInRole("Veterinarian") && !patients.GetAuthorizedVets(pet).Result.Contains(user))
        {
            return Unauthorized();
        }

        return !tutorPets.HasAccess(pet.Id, user) ? Unauthorized() : View(history);
    }
}