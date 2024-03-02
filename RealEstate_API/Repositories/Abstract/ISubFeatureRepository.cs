
using RealEstate_API.Dtos.SubFeatureDtos;

namespace RealEstate_API.Repositories.Abstract
{
    public interface ISubFeatureRepository
    {
        Task<List<ResultSubFeatureDto>> GetAllAsync();
        Task CreateSubFeatureAsync(CreateSubFeatureDto createSubFeatureDto);
        Task UpdateSubFeatureAsync(UpdateSubFeatureDto updateSubFeatureDto);
        Task DeleteSubFeatureAsync(int id);
        Task<GetSubFeatureDto> GetByIdAsync(int id);
    }
}
