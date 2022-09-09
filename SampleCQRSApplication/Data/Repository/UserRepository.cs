using SampleCQRSApplication.Authentication;
using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Data.Repository
{    
    public interface IUserRepository : IGenericRepository<User> {
        Task InsertRange(IList<User> lstEntity);
    }
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDBContext context) : base(context)
        {
        }

        public async Task InsertRange(IList<User> lstEntity)
        {
            await this.dbSet.AddRangeAsync(lstEntity);
        }
    }
}
