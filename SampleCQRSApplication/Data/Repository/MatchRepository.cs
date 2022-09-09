using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Data.Repository
{
    public interface IMatchRepository : IGenericRepository<Match> { }
    internal class MatchRepository : GenericRepository<Match>, IMatchRepository
    {
        public MatchRepository(AppDBContext context) : base(context)
        {
        }
    }
}
