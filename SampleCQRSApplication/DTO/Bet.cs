using SampleCQRSApplication.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.DTO
{
    public class Bet
    {
        public int Id { get; set; }
        public int Decision { get; set; } //0 Home Team win, 1 Visiting Team win, 2 Draw
        public int MatchId { get; set; }
        [ForeignKey(nameof(MatchId))]
        public virtual Match Match { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
