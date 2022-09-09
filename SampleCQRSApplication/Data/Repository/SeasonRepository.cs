using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Data.Repository
{
    public interface ISeasonRepository : IGenericRepository<Season> { }
    internal class SeasonRepository : GenericRepository<Season>, ISeasonRepository
    {
        public SeasonRepository(AppDBContext context) : base(context)
        {
        }
    }
}
