using RealEstate_API.Dtos.TestimonialDtos;

namespace RealEstate_API.Repositories.Abstract
{
    public interface ITestimonialRepository
    {
        Task<List<ResultTestimonialDto>> GetAllAsync();
        Task CreateTestimonialAsync(CreateTestimonialDto createTestimonialDto);
        Task UpdateTestimonialAsync(UpdateTestimonialDto updateTestimonialDto);
        Task DeleteTestimonialAsync(int id);
        Task<GetTestimonialDto> GetByIdAsync(int id);
    }
}
