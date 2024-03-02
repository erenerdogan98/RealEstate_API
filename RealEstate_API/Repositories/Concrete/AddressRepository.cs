using Dapper;
using RealEstate_API.Dtos.AddressDtos;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class AddressRepository(Context context) : IAddressRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DynamicParameters parameters = new();
        public async Task CreateAddressAsync(CreateAddressDto createAddressdDto)
        {
            string query = "INSERT INTO Address (Title,Description,Title2,Phone,Phone2,Email,Location) VALUES (@Title,@Description,@Title2,@Phone,@Phone2,@Email,@Location)";
            parameters.Add("@Title", createAddressdDto.Title);
            parameters.Add("@Description", createAddressdDto.Description);
            parameters.Add("@Title2", createAddressdDto.Title2);
            parameters.Add("@Phone", createAddressdDto.Phone);
            parameters.Add("@Phone2", createAddressdDto.Phone2);
            parameters.Add("@Email", createAddressdDto.Email);
            parameters.Add("@Location", createAddressdDto.Location);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteAddressAsync(int id)
        {
            string query = "DELETE FROM Address WHERE Id=@Id";
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<ResultAddressDto>> GetAllAsync()
        {
            string query = "SELECT * FROM Address";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultAddressDto>(query);
            return values.ToList();
        }

        public async Task<GetAddressDto> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM Address WHERE Id=@Id";
            parameters.Add("@Id", id);
            using var connection = _context.GetConnection();
            var values = await connection.QueryFirstOrDefaultAsync<GetAddressDto>(query, parameters) ?? throw new InvalidOperationException($"The Address with {id} not found!");
            return values;
        }

        public async Task UpdateAddressAsync(UpdateAddressDto updateAddressDto)
        {
            string query = "UPDATE Address SET Title=@Title,Description=@Description,Title2=@Title2,Phone=@Phone,Phone2=@Phone,Email=@Email,Location=@Location WHERE Id=@Id";
            parameters.Add("@Title", updateAddressDto.Title);
            parameters.Add("@Description", updateAddressDto.Description);
            parameters.Add("@Title2", updateAddressDto.Title2);
            parameters.Add("@Phone", updateAddressDto.Phone);
            parameters.Add("@Phone2", updateAddressDto.Phone2);
            parameters.Add("@Email", updateAddressDto.Email);
            parameters.Add("@Location", updateAddressDto.Location);
            parameters.Add("@Id", updateAddressDto.Id);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
