using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.SocialMediaDtos;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController(ISocialMediaRepository socialMediaRepository) : ControllerBase
    {
        private readonly ISocialMediaRepository _socialMediaRepository = socialMediaRepository ?? throw new ArgumentNullException(nameof(socialMediaRepository));

        [HttpGet("socialmediaa")]
        public async Task<IActionResult> SocialMediasAsync()
        {
            var values = await _socialMediaRepository.GetAllAsync() ?? throw new InvalidOperationException("Social Medias not found");
            return Ok(values);
        }

        [HttpPost("createsocialmedia")]
        public async Task<IActionResult> CreateSocialMediaAsync(CreateSocialMediaDto createSocialMedia)
        {
            if (createSocialMedia == null)
            {
                return BadRequest("Invalid Social Media data");
            }
            try
            {
                await _socialMediaRepository.CreateSocialMediaAsync(createSocialMedia);
                return Ok("Social Media  added!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding Social Media : {ex.Message}");
            }
        }
        [HttpDelete("deleteSocialMedia/{id}")]
        public async Task<IActionResult> DeleteSocialMediaAsync(int id)
        {
            try
            {
                await _socialMediaRepository.DeleteSocialMediaAsync(id);
                return Ok($"The Social Media with {id} deleted!");
            }
            catch (Exception ex)
            {

                return BadRequest($"Error deleting Social Media: {ex.Message}");
            }
        }

        [HttpPut("updatesocialmedia")]
        public async Task<IActionResult> UpdateMailSubscribeAsync(UpdateSocialMediaDto updateSocialMedia)
        {
            if (updateSocialMedia == null)
            {
                return BadRequest("Invalid Social Media data");
            }
            int id = updateSocialMedia.Id;
            await _socialMediaRepository.UpdateSocialMediaAsync(updateSocialMedia);
            return Ok($"SocialMedia id : {id} Updated successfully!");
        }

        [HttpGet("SocialMedia{id}")]
        public async Task<IActionResult> GetSocialMediaAsync(int id)
        {
            var testimonial = await _socialMediaRepository.GetByIdAsync(id) ?? throw new InvalidOperationException($"Social Media not found with id : {id}");
            return Ok(testimonial);
        }
    }
}
