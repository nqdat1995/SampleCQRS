﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.DTO
{
    public class TournamentRound
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        [ForeignKey(nameof(TournamentId))]
        public virtual ICollection<Tournament> Tournaments { get; set; }
        public int RoundId { get; set; }
        [ForeignKey(nameof(RoundId))]
        public virtual ICollection<Round>  Rounds { get; set; }
    }
}
