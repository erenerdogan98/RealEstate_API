using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.CategoryDtos;
using RealEstate_API.Dtos.ServiceDtos;
using RealEstate_API.Repositories.Abstract;
using RealEstate_API.Repositories.CategoryRepository;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController(IServiceRepository serviceRepository) : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _serviceRepository.GetAllAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDto createServiceDto)
        {
            if (createServiceDto == null)
            {
                // Eğer categoryDto null ise BadRequest döndür
                return BadRequest("Invalid category data");
            }
            try
            {
                await _serviceRepository.CreateServiceAsync(createServiceDto);
                return Ok("Service added!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding category: {ex.Message}");
            }
        }
    }
}
