using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphas
{
    public class BitHelper
    {
        public static bool AndIsTrue(int a,int b)
        {
            return (a & b) != 0;
        }
    }
}
