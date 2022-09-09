using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Data.Repository
{
    public interface ITournamentSeasonRepository : IGenericRepository<TournamentSeason> { }
    internal class TournamentSeasonRepository : GenericRepository<TournamentSeason>, ITournamentSeasonRepository
    {
        public TournamentSeasonRepository(AppDBContext context) : base(context)
        {
        }
    }
}
