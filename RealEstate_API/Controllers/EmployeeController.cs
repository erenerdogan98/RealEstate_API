using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.EmployeeDtos;
using RealEstate_API.Repositories.Abstract;


namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeRepository employeeRepository) : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));

        [HttpGet("employees")]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            var values = await _employeeRepository.GetAllAsync() ?? throw new InvalidOperationException("Employees not found");
            return Ok(values);
        }

        [HttpPost("createemployee")]
        public async Task<IActionResult> CreateEmployeeAsync(CreateEmployeeDto createEmployee)
        {
            if (createEmployee == null)
            {
                return BadRequest("Invalid Employee data");
            }
            try
            {
                await _employeeRepository.CreateEmployeeAsync(createEmployee);
                return Ok("Employee added!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding Employee : {ex.Message}");
            }
        }
        [HttpDelete("deleteemployee/{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            try
            {
                await _employeeRepository.DeleteEmployeeAsync(id);
                return Ok($"The Employee with {id} deleted!");
            }
            catch (Exception ex)
            {

                return BadRequest($"Error deleting Employee: {ex.Message}");
            }
        }

        [HttpPut("updateemployee")]
        public async Task<IActionResult> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployee)
        {
            if (updateEmployee == null)
            {
                return BadRequest("Invalid Employee data");
            }
            int id = updateEmployee.Id;
            await _employeeRepository.UpdateEmployeeAsync(updateEmployee);
            return Ok($"Employee id : {id} Updated successfully!");
        }

        [HttpGet("GetEmployee/{id}")]
        public async Task<IActionResult> GetContactAsync(int id)
        {
            var testimonial = await _employeeRepository.GetByIdAsync(id) ?? throw new InvalidOperationException($"Employee not found with id : {id}");
            return Ok(testimonial);
        }
    }
}
