using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Utils
{
    public interface ICommonUtils
    {
        string GenerateCode();
    }

    public class CommonUtils : ICommonUtils
    {
        private readonly string AcceptChars = "0123456789";
        public string GenerateCode()
        {
            StringBuilder builder = new StringBuilder("");

            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                builder.Append(AcceptChars[random.Next(0, 9)]);
            }

            return builder.ToString();
        }
    }
}
