using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.DTO
{
    public class Match
    {
        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public virtual Team HomeTeam { get; set; }
        public int VisitingTeamId { get; set; }
        public virtual Team VisitingTeam { get; set; }
        public int RoundId { get; set; }
        [ForeignKey(nameof(RoundId))]
        public virtual Round Round { get; set; }
        public int ScoreHomeTeam { get; set; }
        public int ScoreAwayTeam { get; set; }
        public float RateHomeTeam { get; set; }
        public float RateAwayTeam { get; set; }
        public MatchResult Decision { get; set; }
        public DateTime MatchDate { get; set; }
    }
}
