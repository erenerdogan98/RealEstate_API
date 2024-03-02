using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.ContactDtos;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController(IContactRepository contactRepository) : ControllerBase
    {
        private readonly IContactRepository _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
   
        [HttpGet("contacts")]
        public async Task<IActionResult> ContactsListAsync()
        {
            var values = await _contactRepository.GetAllAsync() ?? throw new InvalidOperationException("Contact not found");
            return Ok(values);
        }

        [HttpPost("createcontact")]
        public async Task<IActionResult> CreateContactAsync(CreateContactDto createContact)
        {
            if (createContact == null)
            {
                return BadRequest("Invalid Contact data");
            }
            try
            {
                await _contactRepository.CreateContactAsync(createContact);
                return Ok("Contact added!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding Contact : {ex.Message}");
            }
        }
        [HttpDelete("deletecontact/{id}")]
        public async Task<IActionResult> DeleteContactAsync(int id)
        {
            try
            {
                await _contactRepository.DeleteContactAsync(id);
                return Ok($"The Contact with {id} deleted!");
            }
            catch (Exception ex)
            {

                return BadRequest($"Error deleting Contact: {ex.Message}");
            }
        }

        [HttpPut("updatecontact")]
        public async Task<IActionResult> UpdateContactAsync(UpdateContactDto updateContact)
        {
            if (updateContact == null)
            {
                return BadRequest("Invalid Contact data");
            }
            int id = updateContact.Id;
            await _contactRepository.UpdateContactAsync(updateContact);
            return Ok($"Contact id : {id} Updated successfully!");
        }

        [HttpGet("Contact{id}")]
        public async Task<IActionResult> GetContactAsync(int id)
        {
            var testimonial = await _contactRepository.GetByIdAsync(id) ?? throw new InvalidOperationException($"Contact not found with id : {id}");
            return Ok(testimonial);
        }
    }
}
