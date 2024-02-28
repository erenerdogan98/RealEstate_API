using RealEstate_API.Dtos.ProductDtos;

namespace RealEstate_API.Repositories.Abstract
{
    public interface IProductRepository
    {
        Task<List<ResultProductDto>> GetAllAsync();
        Task<List<ResultProductWithCategoryDto>> GetProductsWithCategory();
    }
}
