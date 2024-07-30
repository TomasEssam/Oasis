using Todo.Api.DTOs;

namespace Todo.Api.IServices
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoEntityDto>> GetAllAsync();
        Task<TodoEntityDto> GetByIdAsync(int id);
        Task<TodoEntityDto> CreateAsync(TodoEntityDto todoEntityDto);
        Task<bool> UpdateAsync(int id, TodoEntityDto todoEntityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TodoEntityDto>> GetTodosFromApiAsync(int page, int pageSize);
    }

}
