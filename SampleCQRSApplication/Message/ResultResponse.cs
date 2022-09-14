using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Message
{
    public interface IResultResponse
    {
        int Result { get; set; }
    }

    public class ResultResponse : IResultResponse
    {
        public int Result { get; set; }
        public ResultResponse(int result)
        {
            this.Result = result;
        }
        public static ResultResponse BuildResponse(int result)
        {
            return new ResultResponse(result);
        }
    }
}
