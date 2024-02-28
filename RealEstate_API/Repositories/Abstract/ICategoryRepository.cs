using RealEstate_API.Dtos.CategoryDtos;

namespace RealEstate_API.Repositories.Abstract
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task CreateCategory(CreateCategoryDto categoryDto);
        Task UpdateCategory(CategoryDto categoryDto);
        Task DeleteCategory(int id);
        Task<CategoryDto> GetByIdAsync(int id);
    }
}
