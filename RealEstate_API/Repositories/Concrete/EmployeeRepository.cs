using Dapper;
using RealEstate_API.Dtos.EmployeeDtos;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class EmployeeRepository(Context context) : IEmployeeRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DynamicParameters parameters = new();
        public async Task CreateEmployeeAsync(CreateEmployeeDto createContactDto)
        {
            string query = "INSERT INTO Employee (Name,Title,Email,PhoneNumber,ImageUrl,Status) VALUES (@Name,@Title,@Email,@PhoneNumber,@ImageUrl,@Status)";
            parameters.Add("@Name", createContactDto.Name);
            parameters.Add("@Title", createContactDto.Title);
            parameters.Add("@Email", createContactDto.Email);
            parameters.Add("@PhoneNumber", createContactDto.PhoneNumber);
            parameters.Add("@ImageUrl", createContactDto.ImageUrl);
            parameters.Add("@Status", createContactDto.Status);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            string query = "DELETE FROM Employee WHERE Id=@Id";
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<ResultEmployeeDto>> GetAllAsync()
        {
            string query = "SELECT * FROM Employee";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultEmployeeDto>(query);
            return values.ToList();
        }

        public async Task<GetEmployeeDto> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM Employee WHERE Id=@Id";
            parameters.Add("@Id", id);
            using var connection = _context.GetConnection();
            var values = await connection.QueryFirstOrDefaultAsync<GetEmployeeDto>(query, parameters) ?? throw new InvalidOperationException($"The Employee with {id} not found!");
            return values;
        }

        public async Task UpdateEmployeeAsync(UpdateEmployeeDto updateContactDto)
        {
            string query = "UPDATE Employee SET Name=@Name,Title=@Title,Email=@Email,PhoneNumber=@PhoneNumber,ImageUrl=@ImageUrl,Status=@Status WHERE Id=@Id";
            parameters.Add("@Name", updateContactDto.Name);
            parameters.Add("@Title", updateContactDto.Title);
            parameters.Add("@Email", updateContactDto.Email);
            parameters.Add("@PhoneNumber", updateContactDto.PhoneNumber);
            parameters.Add("@ImageUrl", updateContactDto.ImageUrl);
            parameters.Add("@Status", updateContactDto.Status);
            parameters.Add("@Id", updateContactDto.Id);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
