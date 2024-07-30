namespace Todo.Api.DTOs.Identity
{
    public class LoginResultDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public IList<string> Roles { get; set; }

    }
}
