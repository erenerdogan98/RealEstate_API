﻿using Microsoft.AspNetCore.Mvc;

namespace RealEstate_UI.ViewComponents.AdminLayout
{
    public class AdminLayoutHeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
