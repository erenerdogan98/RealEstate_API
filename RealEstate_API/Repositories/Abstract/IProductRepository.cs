using RealEstate_API.Dtos.ProductDtos;

namespace RealEstate_API.Repositories.Abstract
{
    public interface IProductRepository
    {
        Task<List<ResultProductDto>> GetAllAsync();
        Task<List<ResultProductWithCategoryDto>> GetProductsWithCategory();
        Task ChangeDealOfStatusAsync(int id);
        Task<GetProductWithCategoryDto> GetProductWithCategoryAsync(int id);
        Task<List<ResultLastFiveProductsDto>> GetlLastFiveRentedProductsAsync();

    }
}
