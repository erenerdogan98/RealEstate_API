using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.BottomGridDtos;
using RealEstate_API.Dtos.CategoryDtos;
using RealEstate_API.Dtos.ServiceDtos;
using RealEstate_API.Repositories.Abstract;
using RealEstate_API.Repositories.CategoryRepository;
using RealEstate_API.Repositories.Concrete;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BottomGridController(IBottomGridRepository bottomGridRepository) : ControllerBase
    {
        private readonly IBottomGridRepository _bottomGridRepository = bottomGridRepository ?? throw new ArgumentNullException(nameof(bottomGridRepository));
        [HttpGet("Bottomgrids")]
        public async Task<IActionResult> GetAllAsync()
        {
            var values = await _bottomGridRepository.GetAllAsync() ?? throw new InvalidOperationException("Bottom Grids not found");
            return Ok(values);
        }

        [HttpPost("createbottomgrid")]
        public async Task<IActionResult> CreateBottomGrid(CreateBottomGridDto createBottomGrid)
        {
            if (createBottomGrid == null)
            {
                return BadRequest("Invalid Bottom Grid data");
            }
            try
            {
                await _bottomGridRepository.CreateBottomGridAsync(createBottomGrid);
                return Ok("Bottom grid added!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding bottom grid: {ex.Message}");
            }
        }
        [HttpDelete("Deletebottomgrid/{id}")]
        public async Task<IActionResult> DeleteBottomGridAsync(int id)
        {
            try
            {
                await _bottomGridRepository.DeleteBottomGridAsync(id);
                return Ok($"The bottom grid withh {id} deleted!");
            }
            catch (Exception ex)
            {

                return BadRequest($"Error deleting bottom grid: {ex.Message}");
            }
        }

        [HttpPut("updatebottomgrid")]
        public async Task<IActionResult> UpdateBottomGridAsync(UpdateBottomGridDto updateBottomGrid)
        {
            if (updateBottomGrid == null)
            {
                return BadRequest("Invalid Bottom Grid data");
            }
            int id = updateBottomGrid.Id;
            await _bottomGridRepository.UpdateBottomGridAsync(updateBottomGrid);
            return Ok($"Bottom Grid with id : {id} Updated successfully!");
        }

        [HttpGet("GetBottomGrid{id}")]
        public async Task<IActionResult> GetBottomGridAsync(int id)
        {
            var bottomGrid = await _bottomGridRepository.GetByIdAsync(id) ?? throw new InvalidOperationException($"Bottom grid not found with id : {id}");
            return Ok(bottomGrid);
        }
    }
}
