using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.MailSubscribeDtos;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailSubscribeController(IMailSubscribeRepository mailSubscribeRepository) : ControllerBase
    {
        private readonly IMailSubscribeRepository _mailSubscribeRepository = mailSubscribeRepository ?? throw new ArgumentNullException(nameof(mailSubscribeRepository));
        [HttpGet("mailsubscribes")]
        public async Task<IActionResult> MailSubscribesAsync()
        {
            var values = await _mailSubscribeRepository.GetAllAsync() ?? throw new InvalidOperationException("Mail Subscribes not found");
            return Ok(values);
        }

        [HttpPost("mailsubscribe")]
        public async Task<IActionResult> CreateMailSubscribeAsync(CreateMailSubscribeDto createMailSubscribe)
        {
            if (createMailSubscribe == null)
            {
                return BadRequest("Invalid Mail Subscribe data");
            }
            try
            {
                await _mailSubscribeRepository.CreateMailSubscribeAsync(createMailSubscribe);
                return Ok("Mail Subscribe  added!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding Mail Subscribe : {ex.Message}");
            }
        }
        [HttpDelete("deletemailsubscribe/{id}")]
        public async Task<IActionResult> DeleteMailSubscribeAsync(int id)
        {
            try
            {
                await _mailSubscribeRepository.DeleteMailSubscribeAsync(id);
                return Ok($"The Mail Subscribe with {id} deleted!");
            }
            catch (Exception ex)
            {

                return BadRequest($"Error deleting Mail Subscribe: {ex.Message}");
            }
        }

        [HttpPut("updatemailsubscribe")]
        public async Task<IActionResult> UpdateMailSubscribeAsync(UpdateMailSubscribeDto updateMailSubscribe)
        {
            if (updateMailSubscribe == null)
            {
                return BadRequest("Invalid Mail Subscribe data");
            }
            int id = updateMailSubscribe.Id;
            await _mailSubscribeRepository.UpdateMailSubscribeAsync(updateMailSubscribe);
            return Ok($"Mail Subscribe id : {id} Updated successfully!");
        }

        [HttpGet("MailSubscribe{id}")]
        public async Task<IActionResult> GetMailSubscribeAsync(int id)
        {
            var testimonial = await _mailSubscribeRepository.GetByIdAsync(id) ?? throw new InvalidOperationException($"Mail Subscribe not found with id : {id}");
            return Ok(testimonial);
        }
    }
}
