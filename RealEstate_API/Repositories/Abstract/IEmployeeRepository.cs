
using RealEstate_API.Dtos.EmployeeDtos;

namespace RealEstate_API.Repositories.Abstract
{
    public interface IEmployeeRepository
    {
        Task<List<ResultEmployeeDto>> GetAllAsync();
        Task CreateEmployeeAsync(CreateEmployeeDto createContactDto);
        Task UpdateEmployeeAsync(UpdateEmployeeDto updateContactDto);
        Task DeleteEmployeeAsync(int id);
        Task<GetEmployeeDto> GetByIdAsync(int id);
    }
}
