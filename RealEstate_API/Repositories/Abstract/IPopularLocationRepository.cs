using RealEstate_API.Dtos.PopularLocationsDtos;

namespace RealEstate_API.Repositories.Abstract
{
    public interface IPopularLocationRepository
    {
        Task<List<ResultPopularLocationDto>> GetAllAsync();
        Task CreatePopularLocationAsync(CreatePopularLocationDto createPopularLocationDto);
        Task UpdatePopularLocationAsync(UpdatePopularLocationDto updatePopularLocationDto);
        Task DeletePopularLocationAsync(int id);
        Task<GetPopularLocationDto> GetByIdAsync(int id);
    }
}
