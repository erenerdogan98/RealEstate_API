using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_UI.Dtos.CategoryDtos;
using System.Text;

namespace RealEstate_UI.Controllers
{
    [Route("Category")]
    public class CategoryController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        private readonly string dataType = "application/json";

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44349/api/Category/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData) ?? throw new InvalidDataException("..");
                return View(values);
            }
            var errMessage = "Could not retrieve data from API. Please try again later.";
            return Content(errMessage);
        }
        [HttpGet("createcategory")]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost("createcategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(categoryDto);
                StringContent stringContent = new(jsonData, Encoding.UTF8, dataType);

                var responseMessage = await client.PostAsync("https://localhost:44349/api/Category", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                var errorMessage = $"Failed to create category. Status code: {responseMessage.StatusCode}.";
                return Content(errorMessage);

            }
            catch (Exception ex)
            {
                // Handle exceptions, log them, or return an appropriate error message later will use Serilog 
                var errorMessage = $"An error occurred: {ex.Message}";
                return Content(errorMessage);
            }
        }

        [HttpGet("deletecategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44349/api/Category/DeleteCategory/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            var errorMessage = $"Failed to delete category. Status code: {responseMessage.StatusCode}.";
            return Content(errorMessage);
        }

        [HttpGet("updatecategory")]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44349/api/Category/GetCategory/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);
                return View(values);
            }
            var errorMessage = $"Failed to getting a category with ID : {id}. Status code: {responseMessage.StatusCode}.";
            return Content(errorMessage);
        }

        [HttpPost("updatecategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(updateCategory);
                StringContent stringContent = new(jsonData, Encoding.UTF8, dataType);
                var responseMessage = await client.PutAsync("https://localhost:44349/api/Category/UpdateCategory", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                //var errorMessage = $"Failed to update category. Status code: {responseMessage.StatusCode}.";
                //return Content(errorMessage);
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
