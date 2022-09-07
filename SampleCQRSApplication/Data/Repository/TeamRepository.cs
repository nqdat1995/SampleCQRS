using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Data.Repository
{
    public interface ITeamRepository : IGenericRepository<Team> { }
    internal class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(AppDBContext context) : base(context)
        {
        }
    }
}
