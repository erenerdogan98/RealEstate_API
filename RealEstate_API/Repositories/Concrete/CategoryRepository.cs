using RealEstate_API.Dtos.CategoryDtos;
using RealEstate_API.Models.DapperContext;
using Dapper;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.CategoryRepository
{
    public class CategoryRepository(Context context) : ICategoryRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DynamicParameters parameters = new DynamicParameters();

        public async Task CreateCategory(CreateCategoryDto categoryDto)
        {
            string query = "INSERT INTO Category (Name,Status) VALUES (@categoryName,@Status)";
            parameters.Add("@categoryName", categoryDto.Name);
            parameters.Add("@Status", true);

            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteCategory(int id)
        {
            string query = "DELETE FROM Category WHERE Id=@Id";
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            string query = "SELECT * FROM Category";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<CategoryDto>(query);
            return values.ToList();
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM Category WHERE Id=@Id";
            parameters.Add("@Id", id);
            using var connection = _context.GetConnection();
            var values = await connection.QueryFirstOrDefaultAsync<CategoryDto>(query, parameters) ?? throw new InvalidOperationException($"The category with {id} not found!");
            return values;
        }

        public async Task UpdateCategory(CategoryDto categoryDto)
        {
            string query = "UPDATE Category SET Name=@Name,Status=@Status WHERE Id=@Id";
            parameters.Add("@Name", categoryDto.Name);
            parameters.Add("@Status", categoryDto.Status);
            parameters.Add("@Id", categoryDto.Id);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
