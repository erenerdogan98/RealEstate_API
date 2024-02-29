using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.TestimonialDtos;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController(ITestimonialRepository testimonialRepository) : ControllerBase
    {
        private readonly ITestimonialRepository _testimonialRepository = testimonialRepository ?? throw new ArgumentNullException(nameof(testimonialRepository));

        [HttpGet("testimonials")]
        public async Task<IActionResult> GetTestimonials()
        {
            var values = await _testimonialRepository.GetAllAsync() ?? throw new InvalidOperationException("Testimonials not found");
            return Ok(values);
        }

        [HttpPost("createtestimonial")]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonial)
        {
            if (createTestimonial == null)
            {
                return BadRequest("Invalid Testimonials data");
            }
            try
            {
                await _testimonialRepository.CreateTestimonialAsync(createTestimonial);
                return Ok("Testimonial  added!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding Testimonial : {ex.Message}");
            }
        }
        [HttpDelete("deletetestimonial/{id}")]
        public async Task<IActionResult> DeleteTestimonialAsync(int id)
        {
            try
            {
                await _testimonialRepository.DeleteTestimonialAsync(id);
                return Ok($"The Testimonial with {id} deleted!");
            }
            catch (Exception ex)
            {

                return BadRequest($"Error deleting Testimonial: {ex.Message}");
            }
        }

        [HttpPut("updatetestimonial")]
        public async Task<IActionResult> UpdateTestimonialAsync(UpdateTestimonialDto updateTestimonial)
        {
            if (updateTestimonial == null)
            {
                return BadRequest("Invalid Testimonial data");
            }
            int id = updateTestimonial.Id;
            await _testimonialRepository.UpdateTestimonialAsync(updateTestimonial);
            return Ok($"Testimonial id : {id} Updated successfully!");
        }

        [HttpGet("GetTestimonial{id}")]
        public async Task<IActionResult> GetTestimonialAsync(int id)
        {
            var testimonial = await _testimonialRepository.GetByIdAsync(id) ?? throw new InvalidOperationException($"Testimonial not found with id : {id}");
            return Ok(testimonial);
        }
    }
}
