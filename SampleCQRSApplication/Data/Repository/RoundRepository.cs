using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Data.Repository
{
    public interface IRoundRepository : IGenericRepository<Round> { }
    internal class RoundRepository : GenericRepository<Round>, IRoundRepository
    {
        public RoundRepository(AppDBContext context) : base(context)
        {
        }
    }
}
