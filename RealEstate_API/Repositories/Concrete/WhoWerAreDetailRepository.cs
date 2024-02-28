using Dapper;
using RealEstate_API.Dtos.CategoryDtos;
using RealEstate_API.Dtos.WhoWerAreDetailDto;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class WhoWerAreDetailRepository(Context context) : IWhoWerAreDetailRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DynamicParameters parameters = new();
        public async Task CreateAsync(CreateWhoWeAreDetailDto createWhoWeAreDetailDto)
        {
            string query = "INSERT INTO WhoWeAreDetail (Title,SubTitle,Description,Description2) VALUES (@Title,@SubTitle,@Description,@Description2)";
            parameters.Add("@Title", createWhoWeAreDetailDto.Title);
            parameters.Add("@SubTitle", createWhoWeAreDetailDto.SubTitle);
            parameters.Add("@Description", createWhoWeAreDetailDto.Description);
            parameters.Add("@Description2", createWhoWeAreDetailDto.Description2);

            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteAsync(int id)
        {
            string query = "DELETE FROM WhoWeAreDetail WHERE Id=@Id";
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<WhoWeAreDetailDto>> GetAllAsync()
        {
            string query = "SELECT * FROM WhoWeAreDetail";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<WhoWeAreDetailDto>(query);
            return values.ToList();
        }

        public async Task<WhoWeAreDetailDto> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM WhoWeAreDetail WHERE Id=@Id";
            parameters.Add("@Id", id);
            using var connection = _context.GetConnection();
            var values = await connection.QueryFirstOrDefaultAsync<WhoWeAreDetailDto>(query, parameters) ?? throw new InvalidOperationException($"The who we are  with {id} not found!");
            return values;
        }

        public async Task UpdateAsync(WhoWeAreDetailDto updateWhoWeAreDetailDto)
        {
            string query = "UPDATE WhoWeAreDetail SET Title=@Title,SubTitle=@SubTitle,Description=@Description,Description2=@Description2 WHERE Id=@Id";
            parameters.Add("@Title", updateWhoWeAreDetailDto.Title);
            parameters.Add("@SubTitle", updateWhoWeAreDetailDto.SubTitle);
            parameters.Add("@Description", updateWhoWeAreDetailDto.Description);
            parameters.Add("@Description2", updateWhoWeAreDetailDto.Description2);
            parameters.Add("@Id", updateWhoWeAreDetailDto.Id);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
