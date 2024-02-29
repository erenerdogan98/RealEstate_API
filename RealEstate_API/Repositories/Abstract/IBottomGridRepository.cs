using RealEstate_API.Dtos.BottomGridDtos;
using RealEstate_API.Dtos.ServiceDtos;

namespace RealEstate_API.Repositories.Abstract
{
    public interface IBottomGridRepository
    {
        Task<List<ResultBottomGridDto>> GetAllAsync();
        Task CreateBottomGridAsync(CreateBottomGridDto createBottomGridDto);
        Task UpdateBottomGridAsync(UpdateBottomGridDto updateBottomGridDto);
        Task DeleteBottomGridAsync(int id);
        Task<ResultBottomGridDto> GetByIdAsync(int id);
    }
}
