using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Tool.Units;
using IoC.Manager;
using log4net;

namespace Service.Base
{
    public abstract class BaseService
    {
        private ILog _log;
        protected ILog Log
        {
            get { return _log ?? (_log = Log4NetHelper.GetLogger("Service." + this.GetType().Name)); }
        }

        private Stopwatch _watch;

        protected Stopwatch Watch
        {
            get { return _watch ?? (_watch = new Stopwatch()); }
        }

        protected BaseService()
        {

        }
    }
}
