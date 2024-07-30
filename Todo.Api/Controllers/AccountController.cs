using Microsoft.AspNetCore.Mvc;
using Todo.Api.DTOs.Identity;
using Todo.Api.IServices.Identity;

namespace Todo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto dto)
        {
            return Ok(await _userService.Authenticate(dto));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromForm] UserDto dto)
        {
            try
            {
                await _userService.CreateUserAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
