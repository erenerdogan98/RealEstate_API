using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.AddressDtos;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController(IAddressRepository addressRepository) : ControllerBase
    {
        private readonly IAddressRepository _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));

        [HttpGet("addresslist")]
        public async Task<IActionResult> AddressListAsync()
        {
            var values = await _addressRepository.GetAllAsync() ?? throw new InvalidOperationException("Address not found");
            return Ok(values);
        }

        [HttpPost("createaddress")]
        public async Task<IActionResult> CreateContactAsync(CreateAddressDto createAddress)
        {
            if (createAddress == null)
            {
                return BadRequest("Invalid Address data");
            }
            try
            {
                await _addressRepository.CreateAddressAsync(createAddress);
                return Ok("Address added!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding Address : {ex.Message}");
            }
        }
        [HttpDelete("deleteaddress/{id}")]
        public async Task<IActionResult> DeleteAddressAsync(int id)
        {
            try
            {
                await _addressRepository.DeleteAddressAsync(id);
                return Ok($"The Address with {id} deleted!");
            }
            catch (Exception ex)
            {

                return BadRequest($"Error deleting Address: {ex.Message}");
            }
        }

        [HttpPut("updateaddress")]
        public async Task<IActionResult> UpdateAddressAsync(UpdateAddressDto updateAddress)
        {
            if (updateAddress == null)
            {
                return BadRequest("Invalid Contact data");
            }
            int id = updateAddress.Id;
            await _addressRepository.UpdateAddressAsync(updateAddress);
            return Ok($"Address id : {id} Updated successfully!");
        }

        [HttpGet("Address{id}")]
        public async Task<IActionResult> GetAddressAsync(int id)
        {
            var testimonial = await _addressRepository.GetByIdAsync(id) ?? throw new InvalidOperationException($"Address not found with id : {id}");
            return Ok(testimonial);
        }
    }
}
