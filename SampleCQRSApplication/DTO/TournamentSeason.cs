using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.DTO
{
    public class TournamentSeason
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        [ForeignKey(nameof(TournamentId))]
        public virtual Tournament Tournament { get; set; }
        public int SeasonId { get; set; }
        [ForeignKey(nameof(SeasonId))]
        public virtual Season Seasons { get; set; }
    }
}
