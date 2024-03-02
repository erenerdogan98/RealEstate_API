using RealEstate_API.Dtos.ContactDtos;

namespace RealEstate_API.Repositories.Abstract
{
    public interface IContactRepository
    {
        Task<List<ResultContactDto>> GetAllAsync();
        Task CreateContactAsync(CreateContactDto createContactDto);
        Task UpdateContactAsync(UpdateContactDto updateContactDto);
        Task DeleteContactAsync(int id);
        Task<GetContactDto> GetByIdAsync(int id);
    }
}
