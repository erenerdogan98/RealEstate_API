using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_UI.Dtos.PopularLocationDtos;
using System.Text;

namespace RealEstate_UI.Controllers
{
    [Route("{controller}")]
    public class PopularLocationController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        private readonly string dataType = "application/json";

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44349/api/PopularLocation/popularlocations");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultPopularLocationDto>>(jsonData) ?? throw new InvalidDataException("...");
                return View(values);
            }
            var errMessage = "Could not retrieve data from API. Please try again later.";
            return Content(errMessage);
        }

        [HttpGet("createpopularlocation")]
        public IActionResult CreatePopularLocation()
        {
            return View();
        }

        [HttpPost("createpopularlocation")]
        public async Task<IActionResult> CreatePopularLocation(CreatePopularLocationDto createPopularLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createPopularLocation);
                StringContent stringContent = new(jsonData, Encoding.UTF8, dataType);

                var responseMessage = await client.PostAsync("https://localhost:44349/api/PopularLocation/createpopularlocation", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                var errorMessage = $"Failed to Create . Status code: {responseMessage.StatusCode}.";
                return Content(errorMessage);

            }
            catch (Exception ex)
            {
                // Handle exceptions, log them, or return an appropriate error message later will use Serilog 
                var errorMessage = $"An error occurred: {ex.Message}";
                return Content(errorMessage);
            }
        }
        [HttpGet("deletepopularlocation/{id}")]
        public async Task<IActionResult> DeletePopularLocation(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44349/api/PopularLocation/Deletepopularlocation/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            var errorMessage = $"Failed to delete . Status code: {responseMessage.StatusCode}.";
            return Content(errorMessage);
        }

        [HttpGet("updatepopularlocation")]
        public async Task<IActionResult> UpdatePopularLocation(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44349/api/PopularLocation/GetPopularLocation/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdatePopularLocationDto>(jsonData);
                return View(values);
            }
            var errorMessage = $"Failed to getting a data with ID : {id}. Status code: {responseMessage.StatusCode}.";
            return Content(errorMessage);
        }

        [HttpPost("updatepopularlocation")]
        public async Task<IActionResult> UpdatePopularLocation(UpdatePopularLocationDto updatePopularLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(updatePopularLocation);
                StringContent stringContent = new(jsonData, Encoding.UTF8, dataType);
                var responseMessage = await client.PutAsync("https://localhost:44349/api/PopularLocation/updatepopularlocation", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                var errorMessage = await responseMessage.Content.ReadAsStringAsync();
                return Content(errorMessage);

            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred: {ex.Message}";
                return Content(errorMessage);
            }
        }
    }
}
