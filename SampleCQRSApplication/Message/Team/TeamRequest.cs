using System.Text.Json.Serialization;

namespace SampleCQRSApplication.Message
{
    public class TeamRequest
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
