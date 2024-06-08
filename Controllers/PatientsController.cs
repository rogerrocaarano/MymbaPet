using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace c18_98_m_csharp.Controllers
{
    [Authorize (Roles="Veterinarian")]
    public class PatientsController : Controller
    {
        // GET: VetsController
        public ActionResult Index()
        {
            return View();
        }

    }
}
