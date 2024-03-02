using Dapper;
using RealEstate_API.Dtos.SocialMediaDtos;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class SocialMediaRepository(Context context) : ISocialMediaRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DynamicParameters parameters = new();
        public async Task CreateSocialMediaAsync(CreateSocialMediaDto createSocialMediaDto)
        {
            string query = "INSERT INTO SocialMedia (Name,Icon,Url) VALUES (@Name,@Icon,@Url)";
            parameters.Add("@Name", createSocialMediaDto.Name);
            parameters.Add("@Icon", createSocialMediaDto.Icon);
            parameters.Add("@Url", createSocialMediaDto.Url);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteSocialMediaAsync(int id)
        {
            string query = "DELETE FROM SocialMedia WHERE Id=@Id";
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<ResultSocialMediaDto>> GetAllAsync()
        {
            string query = "SELECT * FROM SocialMedia";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultSocialMediaDto>(query);
            return values.ToList();
        }

        public async Task<GetSocialMediaDto> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM SocialMedia WHERE Id=@Id";
            parameters.Add("@Id", id);
            using var connection = _context.GetConnection();
            var values = await connection.QueryFirstOrDefaultAsync<GetSocialMediaDto>(query, parameters) ?? throw new InvalidOperationException($"The service with {id} not found!");
            return values;
        }

        public async Task UpdateSocialMediaAsync(UpdateSocialMediaDto updateSocialMediaDto)
        {
            string query = "UPDATE SubFeature SET Name=@Name,Icon=@Icon,Url=@Url WHERE Id=@Id";
            parameters.Add("@Name", updateSocialMediaDto.Name);
            parameters.Add("@Icon", updateSocialMediaDto.Icon);
            parameters.Add("@Url", updateSocialMediaDto.Url);
            parameters.Add("@Id", updateSocialMediaDto.Id);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
