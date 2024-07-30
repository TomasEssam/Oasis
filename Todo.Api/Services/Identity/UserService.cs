using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Todo.Api.DTOs.Identity;
using Todo.Api.IServices.Identity;
using Todo.Domain.Constants;
using Todo.Domain.Entities.Identity;
using Todo.Domain.Helpers;

namespace Todo.Api.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<LoginResultDto> Authenticate(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);

            if (user != null && user.IsActive)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

                if (result.Succeeded)
                {
                    var claims = await GetUserClaims(user);
                    var token = GenerateAccessToken(claims);
                    var refreshToken = GenerateRefreshToken();
                    user.RefreshToken = refreshToken;
                    SetClaimsInHttpContextManualy(claims);
                    await _userManager.UpdateAsync(user);
                    var userRoles = await _userManager.GetRolesAsync(user);
                    return new LoginResultDto()
                    {
                        Token = token,
                        RefreshToken = refreshToken,
                        UserName = user.UserName,
                        UserId = user.Id,
                        Roles = userRoles,


                    };
                }
                throw new Exception("Invalid User name or password");
            }
            throw new Exception("Invalid User name or password");
        }

        public Task CreateUserAsync(UserDto dto)
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<Claim>> GetUserClaims(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);


            var identity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                new Claim(ApplicationClaims.UserId, user.Id.ToString()),
                new Claim(ApplicationClaims.UserName, user.UserName.ToString()),
                new Claim(ApplicationClaims.Roles, string.Join(",", userRoles)), // used for frontend
               
            }, "Token");

            // used for identity 
            identity.AddClaims(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            return identity.Claims;
        }

        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var nowUtc = DateTime.UtcNow;

            var expiryDuration = double.Parse(_configuration["Token:ExpiryMinutes"]);

            var expires = nowUtc.AddMinutes(expiryDuration);

            var jwt = new JwtSecurityToken(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt); //the method is called WriteToken but returns a string
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private void SetClaimsInHttpContextManualy(IEnumerable<Claim> claims)
        {
            // to make user in httpcontext not empty so when call savechanges 
            // can find username claim to use it in audit information
            var claimsIdentity = new ClaimsIdentity(claims, "Token");
            _httpContextAccessor.HttpContext.User = new ClaimsPrincipal(claimsIdentity);
            AppSecurityContext.Configure(_httpContextAccessor);
        }
    }
}
