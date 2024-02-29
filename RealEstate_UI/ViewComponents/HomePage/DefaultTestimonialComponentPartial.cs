using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_UI.Dtos.TestimonialDtos;

namespace RealEstate_UI.ViewComponents.HomePage
{
    public class DefaultTestimonialComponentPartial(IHttpClientFactory httpClientFactory) : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44349/api/Testimonial/testimonials");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(jsonData);
                return View(values);
            }
            var errMessage = "Could not retrieve data from API. Please try again later.";
            return Content(errMessage);
        }
    }
}
