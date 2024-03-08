using Microsoft.AspNetCore.Mvc;

namespace RealEstate_UI.ViewComponents.EstateAgent
{
    public class EstateAgentLayoutSidebarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
