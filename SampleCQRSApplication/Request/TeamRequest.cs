using System.Text.Json.Serialization;

namespace SampleCQRSApplication.Request
{
    public class TeamRequest
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}
