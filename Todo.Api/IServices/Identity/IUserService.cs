using Todo.Api.DTOs.Identity;

namespace Todo.Api.IServices.Identity
{
    public interface IUserService
    {
        Task<LoginResultDto> Authenticate(LoginDto dto);
        Task CreateUserAsync(UserDto dto);
    }
}
