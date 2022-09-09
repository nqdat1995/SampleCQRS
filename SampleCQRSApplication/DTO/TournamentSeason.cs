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
        public virtual ICollection<Tournament> Tournaments { get; set; }
        public int SeasonId { get; set; }
        [ForeignKey(nameof(SeasonId))]
        public virtual ICollection<Season> Seasons { get; set; }
    }
}
