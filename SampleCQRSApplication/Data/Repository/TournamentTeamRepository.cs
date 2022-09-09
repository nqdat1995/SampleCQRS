using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Data.Repository
{
    public interface ITournamentTeamRepository : IGenericRepository<TournamentTeam> { }
    internal class TournamentTeamRepository : GenericRepository<TournamentTeam>, ITournamentTeamRepository
    {
        public TournamentTeamRepository(AppDBContext context) : base(context)
        {
        }
    }
}
