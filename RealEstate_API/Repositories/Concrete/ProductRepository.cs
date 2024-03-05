using Dapper;
using RealEstate_API.Dtos.ProductDtos;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class ProductRepository(Context context) : IProductRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DynamicParameters parameters = new();

        public async Task ChangeDealOfStatusAsync(int id)
        {
            string query = "UPDATE Product SET DealOfTheDay= CASE WHEN DealOfTheDay=1 THEN 0 ELSE 1 END WHERE Id=@Id";
            using var connection = _context.GetConnection() ?? throw new Exception("Not Connected");
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<ResultProductDto>> GetAllAsync()
        {
            string query = "SELECT * FROM Product";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultProductDto>(query);
            return values.ToList();
        }

        public async Task<List<ResultLastFiveProductsDto>> GetlLastFiveRentedProductsAsync()
        {
            string query = "SELECT TOP(5) Product.Id,Title,Price,City,District,Name,AdvertisementDate FROM Product JOIN Category ON Product.CategoryId=Category.Id WHERE Type='Rented' ORDER BY Id DESC";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultLastFiveProductsDto>(query);
            return values.ToList();
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategory()
        {
            string query = "SELECT Product.Id,Title,Price,CoverImage,City,District,Address,Description,Type,Name, DealOfTheDay FROM Product INNER JOIN Category ON Product.CategoryId=Category.Id";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
            return values.ToList();
        }

        public async Task<GetProductWithCategoryDto> GetProductWithCategoryAsync(int id)
        {
            string query = "SELECT Product.Id,Title,Price,CoverImage,City,District,Address,Description,Type,Name, DealOfTheDay FROM Product INNER JOIN Category ON Product.CategoryId=Category.Id WHERE Product.Id=@Id";
            parameters.Add("@Id", id);
            using var connection = _context.GetConnection();
            var values = await connection.QueryFirstOrDefaultAsync<GetProductWithCategoryDto>(query,parameters) ?? throw new InvalidOperationException($"The Product with {id} not found!");
            return values;
        }
    }
}
