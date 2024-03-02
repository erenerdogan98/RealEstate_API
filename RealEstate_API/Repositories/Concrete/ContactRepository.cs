using Dapper;
using RealEstate_API.Dtos.ContactDtos;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class ContactRepository(Context context) : IContactRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DynamicParameters parameters = new();
        public async Task CreateContactAsync(CreateContactDto createContactDto)
        {
            string query = "INSERT INTO Contact (Name,Subject,Email,Message,SendDate) VALUES (@Name,@Subject,@Email,@Message,@SendDate)";
            parameters.Add("@Name", createContactDto.Name);
            parameters.Add("@Subject", createContactDto.Subject);
            parameters.Add("@Email", createContactDto.Email);
            parameters.Add("@Message", createContactDto.Message);
            parameters.Add("@SendDate", createContactDto.SendDate);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteContactAsync(int id)
        {
            string query = "DELETE FROM Contact WHERE Id=@Id";
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<ResultContactDto>> GetAllAsync()
        {
            string query = "SELECT * FROM Contact";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultContactDto>(query);
            return values.ToList();
        }

        public async Task<GetContactDto> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM Contact WHERE Id=@Id";
            parameters.Add("@Id", id);
            using var connection = _context.GetConnection();
            var values = await connection.QueryFirstOrDefaultAsync<GetContactDto>(query, parameters) ?? throw new InvalidOperationException($"The Contact with {id} not found!");
            return values;
        }

        public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            string query = "UPDATE Contact SET Name=@Name,Subject=@Subject,Email=@Email,Message=@Message,SendDate=@SendDate WHERE Id=@Id";
            parameters.Add("@Name", updateContactDto.Name);
            parameters.Add("@Subject", updateContactDto.Subject);
            parameters.Add("@Email", updateContactDto.Email);
            parameters.Add("@Message", updateContactDto.Message);
            parameters.Add("@SendDate", updateContactDto.SendDate);
            parameters.Add("@Id", updateContactDto.Id);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
