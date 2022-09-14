using SampleCQRSApplication.Authentication;
using System.ComponentModel.DataAnnotations;

namespace SampleCQRSApplication.Message
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public Role Role { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Token = token;
            Role = user.Role;
        }
    }
}
