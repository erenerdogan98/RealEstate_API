using RealEstate_API.Dtos.MailSubscribeDtos;

namespace RealEstate_API.Repositories.Abstract
{
    public interface IMailSubscribeRepository
    {
        Task<List<ResultMailSubscribeDto>> GetAllAsync();
        Task CreateMailSubscribeAsync(CreateMailSubscribeDto createSubFeatureDto);
        Task UpdateMailSubscribeAsync(UpdateMailSubscribeDto updateMailSubscribeDto);
        Task DeleteMailSubscribeAsync(int id);
        Task<GetMailSubscribeDto> GetByIdAsync(int id);
    }
}
