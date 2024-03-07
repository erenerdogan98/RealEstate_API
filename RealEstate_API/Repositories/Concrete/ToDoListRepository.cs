using Dapper;
using RealEstate_API.Dtos.ServiceDtos;
using RealEstate_API.Dtos.ToDoListDtos;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class ToDoListRepository(Context context) : IToDoListRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DynamicParameters parameters = new();
        public async Task CreateToDoListAsync(CreateToDoListDto createToDoListDto)
        {
            string query = "INSERT INTO ToDoList (Description,Status) VALUES (@Description,@Status)";
            parameters.Add("@categoryName", createToDoListDto.Description);
            parameters.Add("@Status", true);

            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteToDoListAsync(int id)
        {
            string query = "DELETE FROM ToDoList WHERE Id=@Id";
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<ResultToDoListDto>> GetAllAsync()
        {
            string query = "SELECT * FROM ToDoList";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultToDoListDto>(query) ?? throw new Exception("Error , will logging");
            return values.ToList();
        }

        public async Task<GetToDoListDto> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM ToDoList WHERE Id=@Id";
            parameters.Add("@Id", id);
            using var connection = _context.GetConnection();
            var values = await connection.QueryFirstOrDefaultAsync<GetToDoListDto>(query, parameters) ?? throw new InvalidOperationException($"The ToDo List with {id} not found!");
            return values;
        }

        public async Task UpdateToDoListAsync(UpdateToDoListDto updateToDoListDto)
        {
            string query = "UPDATE Service SET Description=@Description,Status=@Status WHERE Id=@Id";
            parameters.Add("@Description", updateToDoListDto.Description);
            parameters.Add("@Status", updateToDoListDto.Status);
            parameters.Add("@Id", updateToDoListDto.Id);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
