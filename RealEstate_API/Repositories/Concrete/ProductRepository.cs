using Dapper;
using RealEstate_API.Dtos.ProductDtos;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class ProductRepository(Context context) : IProductRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        public async Task<List<ResultProductDto>> GetAllAsync()
        {
            string query = "SELECT * FROM Product";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultProductDto>(query);
            return values.ToList();
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategory()
        {
            string query = "SELECT Product.Id,Title,Price,City,District,Name FROM Product INNER JOIN Category ON Product.CategoryId=Category.Id";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
            return values.ToList();
        }
    }
}
