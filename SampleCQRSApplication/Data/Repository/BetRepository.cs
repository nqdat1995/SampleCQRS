using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Data.Repository
{    
    public interface IBetRepository : IGenericRepository<Bet> { }
    public class BetRepository : GenericRepository<Bet>, IBetRepository
    {
        public BetRepository(AppDBContext context) : base(context)
        {
        }
    }
}
