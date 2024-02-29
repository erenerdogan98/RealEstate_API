using Dapper;
using RealEstate_API.Dtos.BottomGridDtos;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class BottomGridRepository(Context context) : IBottomGridRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DynamicParameters parameters = new();
        public async Task CreateBottomGridAsync(CreateBottomGridDto createBottomGridDto)
        {
            string query = "INSERT INTO BottomGrid (Icon,Title,Description) VALUES (@Icon,@Title,@Description)";
            parameters.Add("@Icon", createBottomGridDto.Icon);
            parameters.Add("@Title", createBottomGridDto.Title);
            parameters.Add("@Description", createBottomGridDto.Description);

            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteBottomGridAsync(int id)
        {
            string query = "DELETE FROM BottomGrid WHERE Id=@Id";
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<ResultBottomGridDto>> GetAllAsync()
        {
            string query = "SELECT * FROM BottomGrid";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultBottomGridDto>(query);
            return values.ToList();
        }

        public async Task<ResultBottomGridDto> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM BottomGrid WHERE Id=@Id";
            parameters.Add("@Id", id);
            using var connection = _context.GetConnection();
            var values = await connection.QueryFirstOrDefaultAsync<ResultBottomGridDto>(query, parameters) ?? throw new InvalidOperationException($"The service with {id} not found!");
            return values;
        }

        public async Task UpdateBottomGridAsync(UpdateBottomGridDto updateBottomGridDto)
        {
            string query = "UPDATE BottomGrid SET Icon=@Icon,Title=@Title,Description=@Description WHERE Id=@Id";
            parameters.Add("@Icon", updateBottomGridDto.Icon);
            parameters.Add("@Title", updateBottomGridDto.Title);
            parameters.Add("@Description", updateBottomGridDto.Description);
            parameters.Add("@Id", updateBottomGridDto.Id);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
