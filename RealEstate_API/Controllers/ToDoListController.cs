using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.ToDoListDtos;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController(IToDoListRepository toDoListRepository) : ControllerBase
    {
        private readonly IToDoListRepository _toDoListRepository = toDoListRepository ?? throw new ArgumentNullException(nameof(toDoListRepository));

        [HttpGet("ToDos")]
        public async Task<IActionResult> GetToDoLists()
        {
            var toDos = await _toDoListRepository.GetAllAsync() ?? throw new Exception("Will loging");
            return Ok(toDos);
        }
        [HttpPost("createtodolist")]
        public async Task<IActionResult> CreateToDoList(CreateToDoListDto ToDoListDto)
        {
            if (ToDoListDto == null)
            {
                return BadRequest("Invalid ToDoList data");
            }
            try
            {
                await _toDoListRepository.CreateToDoListAsync(ToDoListDto);
                return Ok("ToDoList added!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding ToDoList: {ex.Message}");
            }

        }

        [HttpDelete("DeleteToDoList/{id}")]
        public async Task<IActionResult> DeleteToDoListAsync(int id)
        {
            await _toDoListRepository.DeleteToDoListAsync(id);
            return Ok("ToDoList Deleted.");
        }

        [HttpPut("UpdateToDoList")]
        public async Task<IActionResult> UpdateToDoList(UpdateToDoListDto ToDoList)
        {
            await _toDoListRepository.UpdateToDoListAsync(ToDoList);
            return Ok("ToDoList Updated successfully!");
        }

        [HttpGet("GetToDoList/{id}")]
        public async Task<IActionResult> GetToDoList(int id)
        {
            var ToDoList = await _toDoListRepository.GetByIdAsync(id);
            return Ok(ToDoList);
        }
    }
}
