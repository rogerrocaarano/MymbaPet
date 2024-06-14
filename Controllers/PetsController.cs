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

    public IActionResult Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        if (User.IsInRole("Veterinarian"))
        {
            return RedirectToAction(nameof(PatientDetails), new { id });
        }
        return RedirectToAction(nameof(PetDetails), new { id });
    }

    // GET: Pets/MyPets
    public async Task<IActionResult> MyPets()
    {
        var user = await _userManager.GetUserAsync(User);
        var role = _roleManager.Roles.FirstOrDefault(r => r.Name == "PetTutor");
        var pets = await _petsManager.GetPets(user, role);
        return View(pets);
    }

    // GET: Pets/PetDetails/{PetId}
    public async Task<IActionResult> PetDetails(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        var role = _roleManager.Roles.FirstOrDefault(r => r.Name == "PetTutor");
        var userPets = await _petsManager.GetPets(user, role);
        var pet = userPets.Find(x => x.Id == id.Value);
        // var pet = _petsManager.GetPet(user, id.Value);
        if (pet == null)
        {
            return NotFound();
        }
        
        return View(pet);
    }

    // POST: Pets/PetDetails/{PetId}
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
        var role = _roleManager.Roles.FirstOrDefault(r => r.Name == "PetTutor");
        var userPets = await _petsManager.GetPets(user, role);
        var petToUpdate = userPets.Find(x => x.Id == id);
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

    // GET: Pets/ShareWithVet/{PetId}
    public async Task<IActionResult> ShareWithVet(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        var role = _roleManager.Roles.FirstOrDefault(r => r.Name == "PetTutor");
        var userPets = await _petsManager.GetPets(user, role);
        var pet = userPets.Find(x => x.Id == id.Value);
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

    // GET: Pets/PatientDetails/{PetId}
    [Authorize(Roles = "Veterinarian")]
    public async Task<IActionResult> PatientDetails(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        var role = _roleManager.Roles.FirstOrDefault(r => r.Name == "Veterinarian");
        var userPets = await _petsManager.GetPets(user, role);
        var pet = userPets.Find(x => x.Id == id.Value);
        if (pet == null)
        {
            return NotFound();
        }

        return View(pet);
    }
    
    // todo POST: Pets/PatientsDetails/{PetId}

    // todo GET: Pets/MyPatients/AddNew
    // todo POST: Pets/MyPatients/AddNew

    // GET: Pets/AddPatientByCode
    [Authorize(Roles = "Veterinarian")]
    public async Task<IActionResult> AddPatientByCode()
    {
        return View();
    }

    // POST: Pets/AddPatientByCode
    [Authorize(Roles = "Veterinarian")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddPatientByCode([Bind("Code")] string code)
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
            return RedirectToAction(nameof(AddPatientByCode));
        }

        return RedirectToAction(nameof(MyPatients));
    }
}