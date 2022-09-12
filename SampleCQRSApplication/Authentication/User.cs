using SampleCQRSApplication.DTO;
using System.Text.Json.Serialization;

namespace SampleCQRSApplication.Authentication
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; } = Role.User;
        public UserStatus Status { get; set; } = UserStatus.Validating;
        public virtual ICollection<SendMail> SendMails { get; set; }
    }
}
