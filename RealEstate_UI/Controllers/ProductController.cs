using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RealEstate_UI.Dtos.CategoryDtos;
using RealEstate_UI.Dtos.ProductDtos;

namespace RealEstate_UI.Controllers
{
    [Route("{controller}")]
    public class ProductController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44349/api/Product/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData) ?? throw new InvalidDataException("...");
                return View(values);
            }
            var errMessage = "Could not retrieve data from API. Please try again later.";
            return Content(errMessage);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44349/api/Category/Categories");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData) ?? throw new InvalidDataException("...");

                var categoryValues = (from x in values.ToList()
                                      select new SelectListItem
                                      {
                                          Text = x.Name,
                                          Value = x.Id.ToString()
                                      }).ToList();

                ViewBag.v = categoryValues;
                return View();
            }
            return View();
        }
    }
}
