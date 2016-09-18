using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CustomerException
{
    public class ServiceLayoutException : CommonException
    {
        public ServiceLayoutException(string msg, object data = null) : base(msg, data)
        {
        }
    }
}
