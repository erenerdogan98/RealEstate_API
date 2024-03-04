using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_UI.Dtos.WhoWeAreDtos;
using System.Net.Http;
using System.Text;

namespace RealEstate_UI.Controllers
{
    [Route("{controller}")]
    public class WhoWeAreController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        private readonly string dataType = "application/json";

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44349/api/WhoWeAreDetail/WhoWeAreDetails");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultWhoWeAreDetailDto>>(jsonData) ?? throw new InvalidDataException("...");
                return View(values);
            }
            var errMessage = "Could not retrieve data from API. Please try again later.";
            return Content(errMessage);
        }

        [HttpGet("createwhoweare")]
        public IActionResult CreateWhoWeAreDetail()
        {
            return View();
        }

        [HttpPost("createwhoweare")]
        public async Task<IActionResult> CreateWhoWeAreDetail(CreateWhoWeAreDetailDto createWhoWeAreDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createWhoWeAreDetail);
                StringContent stringContent = new(jsonData, Encoding.UTF8, dataType);

                var responseMessage = await client.PostAsync("https://localhost:44349/api/WhoWeAreDetail/Create", stringContent);

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
        [HttpGet("deletewhowearedetails/{id}")]
        public async Task<IActionResult> DeleteWhoWeAreDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44349/api/WhoWeAreDetail/DeleteWhoWeAreDetail/{id}");
            if (responseMessage.IsSuccessStatusCode)    
            {
                return RedirectToAction("Index");
            }
            var errorMessage = $"Failed to delete . Status code: {responseMessage.StatusCode}.";
            return Content(errorMessage);
        }

        [HttpGet("updatewhoweare")]
        public async Task<IActionResult> UpdateWhoWeAreDetails(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44349/api/WhoWeAreDetail/GetWhoWeAreDetail/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateWhoWeAreDetailDto>(jsonData);
                return View(values);
            }
            var errorMessage = $"Failed to getting a data with ID : {id}. Status code: {responseMessage.StatusCode}.";
            return Content(errorMessage);
        }

        [HttpPost("updatewhoweare")]
        public async Task<IActionResult> UpdateWhoWeAreDetails(UpdateWhoWeAreDetailDto updateEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(updateEmployeeDto);
                StringContent stringContent = new(jsonData, Encoding.UTF8, dataType);
                var responseMessage = await client.PutAsync("https://localhost:44349/api/WhoWeAreDetail/UpdatewhoWeAreDetail", stringContent);
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
