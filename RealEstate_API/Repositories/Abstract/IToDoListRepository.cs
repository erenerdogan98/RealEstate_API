using RealEstate_API.Dtos.ToDoListDtos;

namespace RealEstate_API.Repositories.Abstract
{
    public interface IToDoListRepository
    {
        Task<List<ResultToDoListDto>> GetAllAsync();
        Task CreateToDoListAsync(CreateToDoListDto createToDoListDto);
        Task UpdateToDoListAsync(UpdateToDoListDto updateToDoListDto);
        Task DeleteToDoListAsync(int id);
        Task<GetToDoListDto> GetByIdAsync(int id);
    }
}
