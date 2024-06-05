using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models;
using c18_98_m_csharp.Services.Pets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace c18_98_m_csharp.Controllers;

[Authorize]
public class PetsController(
    ApplicationDbContext context,
    UserManager<AppUser> userManager,
    PetManager petManager)
    : Controller
{
    // GET: Pets
    public async Task<IActionResult> Index()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            // redirect to Area Identity Account Login
            return RedirectToPage("Identity/Account/Login");
        }

        var userPets = await petManager.GetPets(user);
        return View(userPets);
    }

    // GET: Pets/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            // redirect to Area Identity Account Login
            return RedirectToPage("Identity/Account/Login");
        }

        if (id == null || !petManager.UserIsPetTutor(user, id.Value))
        {
            return NotFound();
        }

        var pet = await context.Pets
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pet == null)
        {
            return NotFound();
        }

        return View(pet);
    }


    // GET: Pets/AddPet
    public IActionResult AddPet()
    {
        return View();
    }

    // POST: Pets/AddPet
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddPet(
        [Bind("Id,Name,Breed,Color,Species,Birthdate,Sex,MicrochipId,Notes")]
        Pet pet)
    {
        if (ModelState.IsValid)
        {
            pet.Id = Guid.NewGuid();
            context.Add(pet);
            await context.SaveChangesAsync();
            // register pet tutor
            var user = await userManager.GetUserAsync(User);
            await petManager.AssignPetToTutor(pet, user);
            return RedirectToAction(nameof(Index));
        }

        return View(pet);
    }

    // POST: Pets/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id,
        [Bind("Id,Name,Breed,Color,Species,Birthdate,Sex,MicrochipId,Notes")]
        Pet pet)
    {
        if (id != pet.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(pet);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(pet.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        return View(pet);
    }

    // GET: Pets/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            // redirect to Area Identity Account Login
            return RedirectToPage("Identity/Account/Login");
        }

        if (id == null || !petManager.UserIsPetTutor(user, id.Value))
        {
            return NotFound();
        }

        var pet = await context.Pets
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pet == null)
        {
            return NotFound();
        }

        return View(pet);
    }

    // POST: Pets/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var pet = await context.Pets.FindAsync(id);
        if (pet != null)
        {
            context.Pets.Remove(pet);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PetExists(Guid id)
    {
        return context.Pets.Any(e => e.Id == id);
    }
}