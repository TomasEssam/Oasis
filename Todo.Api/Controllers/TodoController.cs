using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.DTOs;
using Todo.Api.IServices;

namespace Todo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var todoEntities = await _todoService.GetAllAsync();
            return Ok(todoEntities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var todoEntity = await _todoService.GetByIdAsync(id);
            if (todoEntity == null) return NotFound();
            return Ok(todoEntity);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoEntityDto todoEntityDto)
        {
            var createdTodoEntity = await _todoService.CreateAsync(todoEntityDto);
            return Ok();
            //return CreatedAtAction(nameof(Get), new { id = createdTodoEntity.Id }, createdTodoEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TodoEntityDto todoEntityDto)
        {
            var isUpdated = await _todoService.UpdateAsync(id, todoEntityDto);
            if (!isUpdated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _todoService.DeleteAsync(id);
            if (!isDeleted) return NotFound();
            return NoContent();
        }

        [HttpGet("GetTodosExternal")]
        public async Task<IActionResult> GetTodos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var todos = await _todoService.GetTodosFromApiAsync(page, pageSize);
            return Ok(todos);
        }
    }
}
