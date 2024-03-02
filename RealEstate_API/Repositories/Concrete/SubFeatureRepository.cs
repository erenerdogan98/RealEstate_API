using Dapper;
using RealEstate_API.Dtos.SubFeatureDtos;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class SubFeatureRepository(Context context) : ISubFeatureRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DynamicParameters parameters = new();
        public async Task CreateSubFeatureAsync(CreateSubFeatureDto createSubFeatureDto)
        {
            string query = "INSERT INTO SubFeature (Icon,TopTitle,MainTitle,Description,SubTitle) VALUES (@Icon,@TopTitle,@MainTitle,@Description,@SubTitle)";
            parameters.Add("@Icon", createSubFeatureDto.Icon);
            parameters.Add("@TopTitle", createSubFeatureDto.TopTitle);
            parameters.Add("@MainTitle", createSubFeatureDto.MainTitle);
            parameters.Add("@Description", createSubFeatureDto.Description);
            parameters.Add("@SubTitle", createSubFeatureDto.SubTitle);

            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteSubFeatureAsync(int id)
        {
            string query = "DELETE FROM SubFeature WHERE Id=@Id";
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<ResultSubFeatureDto>> GetAllAsync()
        {
            string query = "SELECT * FROM SubFeature";
            using var connection = _context.GetConnection();
            var values = await connection.QueryAsync<ResultSubFeatureDto>(query);
            return values.ToList();
        }

        public async Task<GetSubFeatureDto> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM SubFeature WHERE Id=@Id";
            parameters.Add("@Id", id);
            using var connection = _context.GetConnection();
            var values = await connection.QueryFirstOrDefaultAsync<GetSubFeatureDto>(query, parameters) ?? throw new InvalidOperationException($"The service with {id} not found!");
            return values;
        }

        public async Task UpdateSubFeatureAsync(UpdateSubFeatureDto updateSubFeatureDto)
        {
            string query = "UPDATE SubFeature SET Icon=@Icon,TopTitle=@TopTitle,MainTitle=@MainTitle,Description=@Description,SubTitle=SubTitle WHERE Id=@Id";
            parameters.Add("@Icon", updateSubFeatureDto.Icon);
            parameters.Add("@TopTitle", updateSubFeatureDto.TopTitle);
            parameters.Add("@MainTitle", updateSubFeatureDto.MainTitle);
            parameters.Add("@Description", updateSubFeatureDto.Description);
            parameters.Add("@SubTitle", updateSubFeatureDto.SubTitle);
            parameters.Add("@Id", updateSubFeatureDto.Id);
            using var connection = _context.GetConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
