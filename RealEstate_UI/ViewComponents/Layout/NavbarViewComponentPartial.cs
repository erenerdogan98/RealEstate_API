using Microsoft.AspNetCore.Mvc;

namespace RealEstate_UI.ViewComponents.Layout
{
    public class NavbarViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
