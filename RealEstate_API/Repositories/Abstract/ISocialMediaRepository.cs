using RealEstate_API.Dtos.SocialMediaDtos;

namespace RealEstate_API.Repositories.Abstract
{
    public interface ISocialMediaRepository
    {
        Task<List<ResultSocialMediaDto>> GetAllAsync();
        Task CreateSocialMediaAsync(CreateSocialMediaDto createSocialMediaDto);
        Task UpdateSocialMediaAsync(UpdateSocialMediaDto updateSocialMediaDto);
        Task DeleteSocialMediaAsync(int id);
        Task<GetSocialMediaDto> GetByIdAsync(int id);
    }
}
