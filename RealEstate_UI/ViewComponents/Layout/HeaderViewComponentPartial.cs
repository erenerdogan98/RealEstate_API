using Microsoft.AspNetCore.Mvc;

namespace RealEstate_UI.ViewComponents.Layout
{
    public class HeaderViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
