using c18_98_m_csharp.Core;
using c18_98_m_csharp.Models.ClinicalHistories;
using c18_98_m_csharp.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace c18_98_m_csharp.Controllers;

[Authorize]
public class ClinicalHistoriesController(
    UserManager<AppUser> _userManager,
    ClinicalHistoryManager _clinicalHistoryManager) : Controller
{
    // GET: ClinicalHistories/Details/{ClinicalHistoryId}
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var history = await _clinicalHistoryManager.GetHistory(id.Value, user);
        if (history == null)
        {
            return NotFound();
        }

        var entries = await _clinicalHistoryManager.GetEntries(history);
        ViewData["History"] = history;
        return View(entries);
    }

    // GET: ClinicalHistories/AddEntry/{ClinicalHistoryId}
    [Authorize(Roles = "Veterinarian")]
    public async Task<IActionResult> AddEntry(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var history = await _clinicalHistoryManager.GetHistory(id.Value, user);
        if (history == null)
        {
            return NotFound();
        }

        ViewData["History"] = history;
        return View();
    }

    // POST: ClinicalHistories/AddEntry/{ClinicalHistoryId}
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Veterinarian")]
    public async Task<IActionResult> AddEntry(Guid? id,
        [Bind("ServiceType,PetWeight,ConsultReason,Observations,Diagnosis,Treatment")] ClinicalHistoryEntry entry)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var history = await _clinicalHistoryManager.GetHistory(id.Value, user);
        if (history == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(entry);
        }

        await _clinicalHistoryManager.AddEntry(history, entry, user);
        return RedirectToAction(nameof(Details), new { id });
    }
}