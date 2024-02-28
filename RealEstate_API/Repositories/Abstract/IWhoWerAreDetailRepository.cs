using RealEstate_API.Dtos.WhoWerAreDetailDto;

namespace RealEstate_API.Repositories.Abstract
{
    public interface IWhoWerAreDetailRepository
    {
        Task<List<WhoWeAreDetailDto>> GetAllAsync();
        Task CreateAsync(CreateWhoWeAreDetailDto createWhoWeAreDetailDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(WhoWeAreDetailDto updateWhoWeAreDetailDto);
        Task<WhoWeAreDetailDto> GetByIdAsync(int id);
    }
}
