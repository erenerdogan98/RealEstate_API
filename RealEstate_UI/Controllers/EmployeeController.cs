using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_UI.Dtos.EmployeeDtos;
using RealEstate_UI.Services;
using System.Text;

namespace RealEstate_UI.Controllers
{
    [Authorize]
    [Route("{controller}")]
    public class EmployeeController(IHttpClientFactory httpClientFactory,ILoginService loginService) : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        private readonly ILoginService _loginService = loginService;
        private readonly string dataType = "application/json";

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var currentUser = User.Claims;
            var currentUserId = _loginService.GetCurrentUserId ?? throw new ArgumentNullException("will logging.");
            var token = User.Claims.FirstOrDefault(x => x.Type == "realestatetoken") ?? throw new InvalidDataException() ; // 'realestatetoken' in LoginController , Claim ,           
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44349/api/Employee/employees");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultEmployeeDto>>(jsonData) ?? throw new InvalidDataException("...");
                return View(values);
            }
            var errMessage = "Could not retrieve data from API. Please try again later.";
            return Content(errMessage);
        }

        [HttpGet("createemployee")]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost("createemployee")]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto createEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createEmployee);
                StringContent stringContent = new(jsonData, Encoding.UTF8, dataType);

                var responseMessage = await client.PostAsync("https://localhost:44349/api/Employee/createemployee", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                var errorMessage = $"Failed to create Employee. Status code: {responseMessage.StatusCode}.";
                return Content(errorMessage);

            }
            catch (Exception ex)
            {
                // Handle exceptions, log them, or return an appropriate error message later will use Serilog 
                var errorMessage = $"An error occurred: {ex.Message}";
                return Content(errorMessage);
            }
        }
        [HttpGet("deleteemployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44349/api/Employee/deleteemployee/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            var errorMessage = $"Failed to delete employee. Status code: {responseMessage.StatusCode}.";
            return Content(errorMessage);
        }

        [HttpGet("updateemployee")]
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44349/api/Employee/GetEmployee/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateEmployeeDto>(jsonData);
                return View(values);
            }
            var errorMessage = $"Failed to getting a employee with ID : {id}. Status code: {responseMessage.StatusCode}.";
            return Content(errorMessage);
        }

        [HttpPost("updateemployee")]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
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
                var responseMessage = await client.PutAsync("https://localhost:44349/api/Employee/updateemployee", stringContent);
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
