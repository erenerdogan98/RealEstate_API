using Dapper;
using RealEstate_API.Dtos.MailSubscribeDtos;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class MailSubscribeRepository(Context context) : IMailSubscribeRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DynamicParameters parameters = new();
        public async Task CreateMailSubscribeAsync(CreateMailSubscribeDto createSubFeatureDto)
        {
            string query = "INSERT INTO MailSubscribe (Email) VALUES (@Email)";
            parameters.Add("@Email", createSubFeatureDto.Email);

            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteMailSubscribeAsync(int id)
        {
            string query = "DELETE FROM MailSubscribe WHERE Id=@Id";
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<ResultMailSubscribeDto>> GetAllAsync()
        {
            string query = "SELECT * FROM MailSubscribe";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultMailSubscribeDto>(query);
            return values.ToList();
        }

        public async Task<GetMailSubscribeDto> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM MailSubscribe WHERE Id=@Id";
            parameters.Add("@Id", id);
            using var connection = _context.GetConnection();
            var values = await connection.QueryFirstOrDefaultAsync<GetMailSubscribeDto>(query, parameters) ?? throw new InvalidOperationException($"The service with {id} not found!");
            return values;
        }

        public async Task UpdateMailSubscribeAsync(UpdateMailSubscribeDto updateMailSubscribeDto)
        {
            string query = "UPDATE PopularLocation SET Email=@Email WHERE Id=@Id";
            parameters.Add("@Email", updateMailSubscribeDto.Email);
            parameters.Add("@Id", updateMailSubscribeDto.Id);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
