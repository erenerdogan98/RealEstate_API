using Microsoft.AspNetCore.Mvc;

namespace RealEstate_UI.ViewComponents.HomePage
{
    public class _DefaultHomePageProducts : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
