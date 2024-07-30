using Todo.Domain.Entities.Identity;

namespace Todo.Api.IServices.Identity
{
    public interface IRoleService
    {
        List<ApplicationRole> GetRoles();
        Task<bool> CreateRoleAsync(string roleName);
    }
}
