using Microsoft.AspNetCore.Identity;
using Todo.Api.IServices.Identity;
using Todo.Domain.Configurations;
using Todo.Domain.Entities.Context;
using Todo.Domain.Entities.Identity;

namespace Todo.Api.Services.Identity
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly TodoContext _context;
        public RoleService(RoleManager<ApplicationRole> roleManager, TodoContext todoContext )
        {
            _roleManager = roleManager;
            _context = todoContext;
        }
        public async Task<bool> CreateRoleAsync(string roleName)
        {

          var o =  await _roleManager.CreateAsync(new ApplicationRole
            {
                Name = roleName,
                NormalizedName = roleName
            });
            return true;
        }

        public List<ApplicationRole> GetRoles()
        {
           return  _context.Roles.ToList();
        }
    }
}
