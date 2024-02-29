using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.PopularLocationsDtos;
using RealEstate_API.Repositories.Abstract;


namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopularLocationController(IPopularLocationRepository popularLocationRepository) : ControllerBase
    {
        private readonly IPopularLocationRepository _popularLocationRepository = popularLocationRepository ?? throw new ArgumentNullException(nameof(popularLocationRepository));
        [HttpGet("popularlocations")]
        public async Task<IActionResult> GetPopularLocations()
        {
            var values = await _popularLocationRepository.GetAllAsync() ?? throw new InvalidOperationException("popular locations not found");
            return Ok(values);
        }

        [HttpPost("createpopularlocation")]
        public async Task<IActionResult> CreatePopularLocation(CreatePopularLocationDto createPopularLocation)
        {
            if (createPopularLocation == null)
            {
                return BadRequest("Invalid Popular Location data");
            }
            try
            {
                await _popularLocationRepository.CreatePopularLocationAsync(createPopularLocation);
                return Ok("Popular Location  added!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding Popular Location : {ex.Message}");
            }
        }
        [HttpDelete("Deletepopularlocation/{id}")]
        public async Task<IActionResult> DeleteBottomGridAsync(int id)
        {
            try
            {
                await _popularLocationRepository.DeletePopularLocationAsync(id);
                return Ok($"The Popular Location with {id} deleted!");
            }
            catch (Exception ex)
            {

                return BadRequest($"Error deleting Popular Location: {ex.Message}");
            }
        }

        [HttpPut("updatepopularlocation")]
        public async Task<IActionResult> UpdateBottomGridAsync(UpdatePopularLocationDto updatePopularLocation)
        {
            if (updatePopularLocation == null)
            {
                return BadRequest("Invalid Popular Location data");
            }
            int id = updatePopularLocation.Id;
            await _popularLocationRepository.UpdatePopularLocationAsync(updatePopularLocation);
            return Ok($"Popular Location with id : {id} Updated successfully!");
        }

        [HttpGet("GetPopularLocation{id}")]
        public async Task<IActionResult> GetBottomGridAsync(int id)
        {
            var bottomGrid = await _popularLocationRepository.GetByIdAsync(id) ?? throw new InvalidOperationException($"Popular Location not found with id : {id}");
            return Ok(bottomGrid);
        }
    }
}
