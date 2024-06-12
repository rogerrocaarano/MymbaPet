using System.Threading.Tasks;
using c18_98_m_csharp.Core;
using c18_98_m_csharp.Models.Identity;
using Microsoft.AspNetCore.Authorization;
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

    // GET: MyPets
    public async Task<IActionResult> MyPets()
    {
        var user = await _userManager.GetUserAsync(User);
        var pets = await _petsManager.GetPets(user, null);
        return View(pets);
    }

    // GET: MyPets/Details/{PetId}
    // POST: MyPets/Details/{PetId}

    // GET: MyPets/AddNew
    public async Task<IActionResult> AddNew()
    {
        return View();
    }
    // POST: MyPets/AddNew

    // GET: MyPets/ShareCode/{PetId}


    // VETERINARIAN:

    // GET: MyPatients

    // GET: MyPatients/Details/{PetId}

    // GET: MyPatients/AddNew
    // POST: MyPatients/AddNew

    // GET: MyPatients/AddBySharedCode
}