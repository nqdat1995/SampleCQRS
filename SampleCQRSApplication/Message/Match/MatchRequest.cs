using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Message
{
    public class MatchRequest
    {
        public int HomeTeamId { get; set; }
        public int VisitingTeamId { get; set; }
        public int RoundId { get; set; }
        public int ScoreHomeTeam { get; set; }
        public int ScoreAwayTeam { get; set; }
        public float RateHomeTeam { get; set; }
        public float RateAwayTeam { get; set; }
        public DateTime MatchDate { get; set; }
    }
}
