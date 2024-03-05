using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_UI.Dtos.ProductDtos;

namespace RealEstate_UI.ViewComponents.Dashboard
{
    public class DashboardLastFiveProductsComponentPartial(IHttpClientFactory httpClientFactory) : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44349/api/Product/LastFiveProducts");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var lastFiveProducts = JsonConvert.DeserializeObject<List<ResultLastFiveProductsDto>>(jsonData) ?? throw new InvalidDataException("...");
                return View(lastFiveProducts);
            }
            var errMessage = "Could not retrieve data from API. Please try again later.";
            return Content(errMessage);
        }
    }
}
