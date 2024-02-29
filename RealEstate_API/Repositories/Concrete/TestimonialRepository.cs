using Dapper;
using RealEstate_API.Dtos.TestimonialDtos;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class TestimonialRepository(Context context) : ITestimonialRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DynamicParameters parameters = new();
        public async Task CreateTestimonialAsync(CreateTestimonialDto createTestimonialDto)
        {
            string query = "INSERT INTO Testimonial (Name,Title,Comment,Status) VALUES (@Name,@Title,@Comment,@Status)";
            parameters.Add("@Name", createTestimonialDto.Name);
            parameters.Add("@Title", createTestimonialDto.Title);
            parameters.Add("@Comment", createTestimonialDto.Comment);
            parameters.Add("@Status", createTestimonialDto.Status);

            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteTestimonialAsync(int id)
        {
            string query = "DELETE FROM Testimonial WHERE Id=@Id";
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<ResultTestimonialDto>> GetAllAsync()
        {
            string query = "SELECT * FROM Testimonial";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultTestimonialDto>(query);
            return values.ToList();
        }

        public async Task<GetTestimonialDto> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM Testimonial WHERE Id=@Id";
            parameters.Add("@Id", id);
            using var connection = _context.GetConnection();
            var values = await connection.QueryFirstOrDefaultAsync<GetTestimonialDto>(query, parameters) ?? throw new InvalidOperationException($"The service with {id} not found!");
            return values;
        }

        public async Task UpdateTestimonialAsync(UpdateTestimonialDto updateTestimonialDto)
        {
            string query = "UPDATE Testimonial SET Name=@Name,Title=@Title,Comment=@Comment,Status=@Status WHERE Id=@Id";
            parameters.Add("@Name", updateTestimonialDto.Name);
            parameters.Add("@Title", updateTestimonialDto.Title);
            parameters.Add("@Comment", updateTestimonialDto.Comment);
            parameters.Add("@Status", updateTestimonialDto.Status);
            parameters.Add("@Id", updateTestimonialDto.Id);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
