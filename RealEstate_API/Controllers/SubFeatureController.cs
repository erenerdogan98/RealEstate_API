using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.SubFeatureDtos;
using RealEstate_API.Dtos.TestimonialDtos;
using RealEstate_API.Repositories.Abstract;
using RealEstate_API.Repositories.Concrete;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubFeatureController(ISubFeatureRepository subFeatureRepository) : ControllerBase
    {
        private readonly ISubFeatureRepository _subFeatureRepository =subFeatureRepository ?? throw new ArgumentNullException(nameof(subFeatureRepository));

        [HttpGet("subfeatures")]
        public async Task<IActionResult> GetSubFeatureAsync()
        {
            var values = await _subFeatureRepository.GetAllAsync() ?? throw new InvalidOperationException("Sub Features not found");
            return Ok(values);
        }

        [HttpPost("createsubfeatures")]
        public async Task<IActionResult> CreateSubFeatureAsync(CreateSubFeatureDto createSubFeature)
        {
            if (createSubFeature == null)
            {
                return BadRequest("Invalid Sub Feature data");
            }
            try
            {
                await _subFeatureRepository.CreateSubFeatureAsync(createSubFeature);
                return Ok("Sub Feature  added!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding Sub Feature : {ex.Message}");
            }
        }
        [HttpDelete("deletesubfeature/{id}")]
        public async Task<IActionResult> DeleteSubFeatureAsync(int id)
        {
            try
            {
                await _subFeatureRepository.DeleteSubFeatureAsync(id);
                return Ok($"The Sub Feature with {id} deleted!");
            }
            catch (Exception ex)
            {

                return BadRequest($"Error deleting Sub Feature: {ex.Message}");
            }
        }

        [HttpPut("updatesubfeature")]
        public async Task<IActionResult> UpdateSubFeatureAsync(UpdateSubFeatureDto updateSubFeature)
        {
            if (updateSubFeature == null)
            {
                return BadRequest("Invalid Sub Feature data");
            }
            int id = updateSubFeature.Id;
            await _subFeatureRepository.UpdateSubFeatureAsync(updateSubFeature);
            return Ok($"Sub Feature id : {id} Updated successfully!");
        }

        [HttpGet("GetSubFeature{id}")]
        public async Task<IActionResult> GetTestimonialAsync(int id)
        {
            var testimonial = await _subFeatureRepository.GetByIdAsync(id) ?? throw new InvalidOperationException($"Sub Feature not found with id : {id}");
            return Ok(testimonial);
        }
    }
}
