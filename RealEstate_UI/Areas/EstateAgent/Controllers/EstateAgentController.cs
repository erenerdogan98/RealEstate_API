using Microsoft.AspNetCore.Mvc;

namespace RealEstate_UI.Areas.EstateAgent.Controllers
{
    public class EstateAgentController : Controller
    {
        [Area("EstateAgent")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
