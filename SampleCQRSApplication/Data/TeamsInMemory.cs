using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Data
{
    public class TeamsInMemory
    {
        private readonly List<Team> _lstTeam;
        public TeamsInMemory()
        {
            _lstTeam = new List<Team>
            {
                new Team { Id = 1, Name ="Viet Nam", IsActive = true },
                new Team { Id = 2, Name ="Laos", IsActive = true },
                new Team { Id = 3, Name ="Cambodia", IsActive = true },
            };
        }
        public List<Team> Teams => _lstTeam;
    }
}
