using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.CategoryDtos;
using RealEstate_API.Dtos.WhoWerAreDetailDto;
using RealEstate_API.Repositories.Abstract;
using RealEstate_API.Repositories.CategoryRepository;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhoWeAreDetailController(IWhoWerAreDetailRepository whoWerAreDetailRepository) : ControllerBase
    {
        private readonly IWhoWerAreDetailRepository _whoWeAreDetailRepository = whoWerAreDetailRepository ?? throw new ArgumentNullException(nameof(whoWerAreDetailRepository));

        [HttpGet("WhoWeAreDetails")]
        public async Task<IActionResult> WhoWeAreDetailList()
        {
            var values = await _whoWeAreDetailRepository.GetAllAsync();
            return Ok(values);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateWhoWeAreDetail(CreateWhoWeAreDetailDto createWhoWeAreDetailDto)
        {
            if (createWhoWeAreDetailDto == null)
            {
                // Eğer categoryDto null ise BadRequest döndür
                return BadRequest("Invalid who we are data");
            }
            try
            {
                await _whoWeAreDetailRepository.CreateAsync(createWhoWeAreDetailDto);
                return Ok("Who we are added!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding operation: {ex.Message}");
            }

        }

        [HttpDelete("DeleteWhoWeAreDetail")]
        public async Task<IActionResult> DeleteWhoWeAreAsync(int id)
        {
            await _whoWeAreDetailRepository.DeleteAsync(id);
            return Ok(" Deleted Successfullu.");
        }

        [HttpPut("UpdatewhoWeAreDetail")]
        public async Task<IActionResult> UpdateWhoWeAreDetailAsync(WhoWeAreDetailDto updateWhoWeAreDetail)
        {
            await _whoWeAreDetailRepository.UpdateAsync(updateWhoWeAreDetail);
            return Ok("Updated successfully!");
        }

        [HttpGet("GetWhoWeAreDetail{id}")]
        public async Task<IActionResult> GetWhoWeAreDetailAsync(int id)
        {
            var category = await _whoWeAreDetailRepository.GetByIdAsync(id);
            return Ok(category);
        }
    }
}
