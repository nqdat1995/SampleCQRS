using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.DTO
{
    public class Match
    {
        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public int RoundId { get; set; }
        public virtual Round Round { get; set; }
        public int ScoreHomeTeam { get; set; }
        public int ScoreAwayTeam { get; set; }
        public float RateHomeTeam { get; set; }
        public float RateAwayTeam { get; set; }
        public DateTime MatchDate { get; set; }
    }
}
