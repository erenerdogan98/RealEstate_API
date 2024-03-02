using Microsoft.AspNetCore.Mvc;

namespace RealEstate_UI.ViewComponents.AdminLayout
{
    public class AdminLayoutFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
