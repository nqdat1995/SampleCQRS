using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Data.Repository
{
    public interface ITournamentRoundRepository : IGenericRepository<TournamentRound> { }
    internal class TournamentRoundRepository : GenericRepository<TournamentRound>, ITournamentRoundRepository
    {
        public TournamentRoundRepository(AppDBContext context) : base(context)
        {
        }
    }
}
