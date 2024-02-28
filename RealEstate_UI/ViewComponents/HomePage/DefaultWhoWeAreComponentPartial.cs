using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_UI.Dtos.WhoWeAreDtos;
using RealEstate_UI.Models;

namespace RealEstate_UI.ViewComponents.HomePage
{
    public class DefaultWhoWeAreComponentPartial(IHttpClientFactory httpClientFactory) : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44349/api/WhoWeAreDetail/WhoWeAreDetails");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultWhoWeAreDetailDto>>(jsonData);

                if (value != null && value.Any())
                {
                    var model = new WhoWeAreComponentModel
                    {
                        Title = value.Select(x => x.Title).FirstOrDefault(),
                        Subtitle = value.Select(x => x.SubTitle).FirstOrDefault(),
                        Description = value.Select(x => x.Description).FirstOrDefault(),
                        Description2 = value.Select(x => x.Description2).FirstOrDefault()
                    };

                    return View(model);
                }
            }
            return View();
        }
    }
}
