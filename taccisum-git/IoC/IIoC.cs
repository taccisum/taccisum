using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC.Manager
{
    public interface IIoC
    {
        T Resolve<T>();
    }
}
