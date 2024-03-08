using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_UI.Dtos.ProductDtos;
using RealEstate_UI.Services;
using System.Net.Http;

namespace RealEstate_UI.Areas.EstateAgent.Controllers
{
    [Area("EstateAgent")]
    [Route("{controller}")]
    public class MyAdvertsController(IHttpClientFactory httpClientFactory,ILoginService loginService) : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        private readonly ILoginService _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
        private readonly string dataType = "application/json";
        
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var id = _loginService.GetCurrentUserId;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44349/api/Product/ProductAdvertListByEmployee/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductAdvertsWithCategoryByEmployeeDto>>(jsonData) ?? throw new InvalidDataException("...");
                return View(values);
            }
            var errMessage = "Could not retrieve data from API. Please try again later.";
            return Content(errMessage);
        }
    }
}
