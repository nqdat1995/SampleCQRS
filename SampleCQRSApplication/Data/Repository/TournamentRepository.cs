using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Data.Repository
{
    public interface ITournamentRepository : IGenericRepository<Tournament> { }
    internal class TournamentRepository : GenericRepository<Tournament>, ITournamentRepository
    {
        public TournamentRepository(AppDBContext context) : base(context)
        {
        }
    }
}
