using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.ServiceDtos;
using RealEstate_API.Repositories.Abstract;


namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController(IServiceRepository serviceRepository) : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));

        [HttpGet("services")]
        public async Task<IActionResult> Index()
        {
            var values = await _serviceRepository.GetAllAsync();
            return Ok(values);
        }
        [HttpPost("CreateService")]
        public async Task<IActionResult> CreateService(CreateServiceDto createServiceDto)
        {
            if (createServiceDto == null)
            {
                return BadRequest("Invalid service data");
            }
            try
            {
                await _serviceRepository.CreateServiceAsync(createServiceDto);
                return Ok("Service added!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding service: {ex.Message}");
            }
        }
        [HttpDelete("DeleteService/{id}")]
        public async Task<IActionResult> DeleteServiceAsync(int id)
        {
            var value = await _serviceRepository.GetByIdAsync(id);
            if (value != null)
            {
                await _serviceRepository.DeleteServiceAsync(id);
                return Ok(" Deleted Successfully.");
            }
            return BadRequest($"Not found data with ID : {id}");

        }

        [HttpPut("UpdateService")]
        public async Task<IActionResult> UpdateServiceAsync(ServiceDto serviceDto)
        {
            await _serviceRepository.UpdateServiceAsync(serviceDto);
            return Ok("Updated successfully!");
        }

        [HttpGet("GetService/{id}")]
        public async Task<IActionResult> GetServiceAsync(int id)
        {
            var category = await _serviceRepository.GetByIdAsync(id);
            return Ok(category);
        }
    }
}
