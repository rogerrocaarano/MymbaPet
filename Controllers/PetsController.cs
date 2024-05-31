using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using c18_98_m_csharp.Data;
using c18_98_m_csharp.Models;
using System.Formats.Asn1;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace c18_98_m_csharp.Controllers
{
    [Authorize]
    public class PetsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PetsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Pets
        public async Task<IActionResult> Index()
        {
            var userPets = await GetUserPets();
            return View(userPets);
        }

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || !UserIsPetTutor(id.Value))
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Breed,Color,Species,Birthdate,Sex,MicrochipId,Description")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                pet.Id = Guid.NewGuid();
                _context.Add(pet);
                await _context.SaveChangesAsync();
                // register pet tutor
                var user = await _userManager.GetUserAsync(User);
                await RegisterPetTutor(user.Id, pet.Id);
                return RedirectToAction(nameof(Index));
            }

            return View(pet);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || !UserIsPetTutor(id.Value))
            {
                return NotFound();
            }

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Breed,Color,Species,Birthdate,Sex,MicrochipId,Description")] Pet pet)
        {
            if (id != pet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
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
            if (id == null || !UserIsPetTutor(id.Value))
            {
                return NotFound();
            }

            var pet = await _context.Pets
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
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(Guid id)
        {
            return _context.Pets.Any(e => e.Id == id);
        }

        private async Task<List<Pet>> GetUserPets()
        {
            var user = await _userManager.GetUserAsync(User);
            return await GetUserPets(user);
        }

        private async Task<List<Pet>> GetUserPets(AppUser user)
        {
            return await _context.AppUserPets
                .Where(x => x.TutorId == user.Id)
                .Select(x => x.Pet)
                .ToListAsync();
        }

        private bool UserIsPetTutor(Guid petId)
        {
            var user = _userManager.GetUserAsync(User).Result;
            return UserIsPetTutor(user, petId);
        }

        private bool UserIsPetTutor(AppUser user, Guid petId)
        {
            var userPets = GetUserPets(user).Result;
            return userPets.Any(x => x.Id == petId);
        }

        private async Task RegisterPetTutor(Guid user, Guid pet)
        {
            var appUserPet = new AppUserPet
            {
                TutorId = user,
                PetId = pet
            };
            _context.Add(appUserPet);
            await _context.SaveChangesAsync();
        }
    }
}
