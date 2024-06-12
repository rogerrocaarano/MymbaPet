using System.Threading.Tasks;
using c18_98_m_csharp.Core;
using c18_98_m_csharp.Models.Identity;
using c18_98_m_csharp.Models.Pets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace c18_98_m_csharp.Controllers;

[Authorize]
public class PetsController : Controller
{
    private readonly PetsManager _petsManager;
    private readonly UserManager<AppUser> _userManager;

    public PetsController(PetsManager petsManager, UserManager<AppUser> userManager)
    {
        _petsManager = petsManager;
        _userManager = userManager;
    }

    // USER:

    // GET: Pets/MyPets
    public async Task<IActionResult> MyPets()
    {
        var user = await _userManager.GetUserAsync(User);
        var pets = await _petsManager.GetPets(user, null);
        return View(pets);
    }

    // GET: Pets/MyPets/Details/{PetId}
    // POST: Pets/MyPets/Details/{PetId}

    // GET: Pets/AddNew
    public async Task<IActionResult> AddNew()
    {
        return View();
    }

    // POST: Pets/AddNew
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddNew([Bind("Id,Name,Species,Birthdate,Notes")] Pet pet)
    {
        if (!ModelState.IsValid)
        {
            return View(pet);
        }

        var user = await _userManager.GetUserAsync(User);
        await _petsManager.Register(pet, user);
        return RedirectToAction(nameof(MyPets));
    }

    // GET: Pets/MyPets/ShareCode/{PetId}


    // VETERINARIAN:

    // GET: Pets/MyPatients

    // GET: Pets/MyPatients/Details/{PetId}

    // GET: Pets/MyPatients/AddNew
    // POST: Pets/MyPatients/AddNew

    // GET: Pets/MyPatients/AddBySharedCode
}