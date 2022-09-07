using System.Text.Json.Serialization;

namespace SampleCQRSApplication.DTO
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Match> HomeMatches { get; set; }
        public virtual ICollection<Match> AwayMatches { get; set; }
        [JsonIgnore]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}
