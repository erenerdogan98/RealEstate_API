﻿using Microsoft.AspNetCore.Mvc;

namespace RealEstate_UI.ViewComponents.HomePage
{
    public class DefaultSubFeatureComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
