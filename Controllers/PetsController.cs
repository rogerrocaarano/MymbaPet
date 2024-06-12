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
    private readonly RoleManager<AppRole> _roleManager;

    public PetsController(
        PetsManager petsManager,
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager)
    {
        _petsManager = petsManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // USER:
    public IActionResult Index()
    {
        if (User.IsInRole("Veterinarian"))
        {
            return RedirectToAction(nameof(MyPatients));
        }

        return RedirectToAction(nameof(MyPets));
    }

    // GET: Pets/MyPets
    public async Task<IActionResult> MyPets()
    {
        var user = await _userManager.GetUserAsync(User);
        var pets = await _petsManager.GetPets(user, null);
        return View(pets);
    }

    // GET: Pets/Details/{PetId}
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        var pet = _petsManager.GetPet(user, id.Value);
        if (pet == null)
        {
            return NotFound();
        }

        return View(pet);
    }

    // POST: Pets/Details/{PetId}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Details(Guid id, [Bind("Id,Name,Birthdate,Notes")] Pet pet)
    {
        if (id != pet.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(pet);
        }

        var user = await _userManager.GetUserAsync(User);
        var petToUpdate = _petsManager.GetPet(user, pet.Id);
        if (petToUpdate == null)
        {
            return NotFound();
        }

        // Update only the fields that can be modified by the user
        petToUpdate.Name = pet.Name;
        petToUpdate.Birthdate = pet.Birthdate;
        petToUpdate.Notes = pet.Notes;

        await _petsManager.Update(petToUpdate);
        return RedirectToAction(nameof(MyPets));
    }

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

    // GET: Pets/ShareCode/{PetId}
    public async Task<IActionResult> ShareCode(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        var pet = _petsManager.GetPet(user, id.Value);
        if (pet == null)
        {
            return Unauthorized();
        }

        var accessCode = await _petsManager.GenerateAccessCode(pet);
        return View(accessCode);
    }


    // VETERINARIAN:

    // GET: Pets/MyPatients
    [Authorize(Roles = "Veterinarian")]
    public async Task<IActionResult> MyPatients()
    {
        var user = await _userManager.GetUserAsync(User);
        var role = _roleManager.Roles.FirstOrDefault(r => r.Name == "Veterinarian");
        var pets = await _petsManager.GetPets(user, role);
        return View(pets);
    }

    // todo GET: Pets/MyPatients/Details/{PetId}
    // todo POST: Pets/MyPatients/Details/{PetId}

    // todo GET: Pets/MyPatients/AddNew
    // todo POST: Pets/MyPatients/AddNew

    // todo GET: Pets/MyPatients/AddBySharedCode
    [Authorize(Roles = "Veterinarian")]
    public async Task<IActionResult> AddBySharedCode()
    {
        return View();
    }
    
    // POST: Pets/MyPatients/AddBySharedCode
    [Authorize (Roles = "Veterinarian")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddBySharedCode([Bind("Code")] string code)
    {
        var accessCode = await _petsManager.GetAccessCode(code);
        if (accessCode == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        var role = _roleManager.Roles.FirstOrDefault(r => r.Name == "Veterinarian");
        
        var result = await _petsManager.UseAccessCode(accessCode, user, role);
        if (!result)
        {
            return RedirectToAction(nameof(AddBySharedCode));
        }
        return RedirectToAction(nameof(MyPatients));
    }
}