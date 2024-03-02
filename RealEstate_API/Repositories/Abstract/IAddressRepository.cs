using RealEstate_API.Dtos.AddressDtos;

namespace RealEstate_API.Repositories.Abstract
{
    public interface IAddressRepository
    {
        Task<List<ResultAddressDto>> GetAllAsync();
        Task CreateAddressAsync(CreateAddressDto createAddressdDto);
        Task UpdateAddressAsync(UpdateAddressDto updateAddressDto);
        Task DeleteAddressAsync(int id);
        Task<GetAddressDto> GetByIdAsync(int id);
    }
}
