using RealEstate_API.Dtos.ServiceDtos;

namespace RealEstate_API.Repositories.Abstract
{
    public interface IServiceRepository
    {
        Task<List<ServiceDto>> GetAllAsync();
        Task CreateServiceAsync(CreateServiceDto createServiceDto);
        Task UpdateServiceAsync(ServiceDto updateServiceDto);
        Task DeleteServiceAsync(int id);
        Task<ServiceDto> GetByIdAsync(int id);
    }
}
