using AutoMapper;
using Newtonsoft.Json;
using Todo.Api.DTOs;
using Todo.Api.IServices;
using Todo.Domain.Entities;
using Todo.Domain.IPresistance;
using Todo.Domain.IRepository;

namespace Todo.Api.Services
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<TodoEntity> _repository;

        public TodoService(HttpClient httpClient, IRepository<TodoEntity> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _httpClient = httpClient;
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TodoEntityDto> CreateAsync(TodoEntityDto todoEntityDto)
        {
            var todoEntity = _mapper.Map<TodoEntity>(todoEntityDto);
            await _repository.AddAsync(todoEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TodoEntityDto>(todoEntity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todoEntity = await _repository.GetByIdAsync(id);
            if (todoEntity == null) return false;

            _repository.Delete(todoEntity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TodoEntityDto>> GetAllAsync()
        {
            var todoEntities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TodoEntityDto>>(todoEntities);
        }

        public async Task<TodoEntityDto> GetByIdAsync(int id)
        {
            var todoEntity = await _repository.GetByIdAsync(id);
            return todoEntity == null ? null : _mapper.Map<TodoEntityDto>(todoEntity);
        }

        public async Task<IEnumerable<TodoEntityDto>> GetTodosFromApiAsync(int page, int pageSize)
        {
            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/todos?_page={page}&_limit={pageSize}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TodoEntityDto>>(content);
        }

        public async Task<bool> UpdateAsync(int id, TodoEntityDto todoEntityDto)
        {
            var todoEntity = await _repository.GetByIdAsync(id);
            if (todoEntity == null) return false;

            _mapper.Map(todoEntityDto, todoEntity);
            _repository.Update(todoEntity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
