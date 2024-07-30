using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.IServices.Identity;
using Todo.Domain.Entities.Identity;

namespace Todo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            await _roleService.CreateRoleAsync(roleName);
            return Ok();
        }


        [HttpGet]
        public ActionResult<IEnumerable<ApplicationRole>> GetRoles(string roleName)
        {
            return Ok(_roleService.GetRoles());
        }
    }
}
