using c18_98_m_csharp.Core;
using c18_98_m_csharp.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace c18_98_m_csharp.Controllers;

public class ClinicalHistoriesController(
    UserManager<AppUser> _userManager,
    ClinicalHistoryManager _clinicalHistoryManager) : Controller
{
    // GET: ClinicalHistories/{ClinicalHistoryId}
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
}