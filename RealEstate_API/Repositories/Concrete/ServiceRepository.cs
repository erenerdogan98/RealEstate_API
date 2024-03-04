using Dapper;
using RealEstate_API.Dtos.ServiceDtos;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class ServiceRepository(Context context) : IServiceRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DynamicParameters parameters = new();
        public async Task CreateServiceAsync(CreateServiceDto createServiceDto)
        {
            string query = "INSERT INTO Service (Name,Status) VALUES (@categoryName,@Status)";
            parameters.Add("@categoryName", createServiceDto.Name);
            parameters.Add("@Status", true);

            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteServiceAsync(int id)
        {
            string query = "DELETE FROM Service WHERE Id=@Id";
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<ServiceDto>> GetAllAsync()
        {
            string query = "SELECT * FROM Service";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ServiceDto>(query);
            return values.ToList();
        }

        public async Task<ServiceDto> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM Service WHERE Id=@Id";
            parameters.Add("@Id", id);
            using var connection = _context.GetConnection();
            var values = await connection.QueryFirstOrDefaultAsync<ServiceDto>(query, parameters) ?? throw new InvalidOperationException($"The service with {id} not found!");
            return values;
        }

        public async Task UpdateServiceAsync(ServiceDto updateServiceDto)
        {
            string query = "UPDATE Service SET Name=@Name,Status=@Status WHERE Id=@Id";
            parameters.Add("@Name", updateServiceDto.Name);
            parameters.Add("@Status", updateServiceDto.Status);
            parameters.Add("@Id", updateServiceDto.Id);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
