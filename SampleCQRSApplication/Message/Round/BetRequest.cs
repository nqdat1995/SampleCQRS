using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Message
{
    public class BetRequest
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public bool IsDraw { get; set; }
    }
}
