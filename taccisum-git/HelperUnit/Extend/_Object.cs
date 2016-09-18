using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tool.Extend
{
    public static class _Object
    {
        public static bool IsNull(this object o)
        {
            return o == null;
        }
    }
}
