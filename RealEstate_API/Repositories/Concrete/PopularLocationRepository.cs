using Dapper;
using RealEstate_API.Dtos.PopularLocationsDtos;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class PopularLocationRepository(Context context) : IPopularLocationRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DynamicParameters parameters = new();
        public async Task CreatePopularLocationAsync(CreatePopularLocationDto createPopularLocationDto)
        {
            string query = "INSERT INTO PopularLocation (CityName,ImageUrl) VALUES (@CityName,@ImageUrl)";
            parameters.Add("@CityName", createPopularLocationDto.CityName);
            parameters.Add("@ImageUrl", createPopularLocationDto.ImageUrl);

            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeletePopularLocationAsync(int id)
        {
            string query = "DELETE FROM PopularLocation WHERE Id=@Id";
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<ResultPopularLocationDto>> GetAllAsync()
        {
            string query = "SELECT * FROM PopularLocation";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultPopularLocationDto>(query);
            return values.ToList();
        }

        public async Task<GetPopularLocationDto> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM PopularLocation WHERE Id=@Id";
            parameters.Add("@Id", id);
            using var connection = _context.GetConnection();
            var values = await connection.QueryFirstOrDefaultAsync<GetPopularLocationDto>(query, parameters) ?? throw new InvalidOperationException($"The service with {id} not found!");
            return values;
        }

        public async Task UpdatePopularLocationAsync(UpdatePopularLocationDto updatePopularLocationDto)
        {
            string query = "UPDATE PopularLocation SET CityName=@CityName,ImageUrl=@ImageUrl WHERE Id=@Id";
            parameters.Add("@CityName", updatePopularLocationDto.CityName);
            parameters.Add("@ImageUrl", updatePopularLocationDto.ImageUrl);
            parameters.Add("@Id", updatePopularLocationDto.Id);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
