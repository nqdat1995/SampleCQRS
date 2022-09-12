using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Data.Repository
{
    public interface ISendMailRepository : IGenericRepository<SendMail> { }
    public class SendMailRepository : GenericRepository<SendMail>, ISendMailRepository
    {
        public SendMailRepository(AppDBContext context) : base(context)
        {
        }
    }
}
