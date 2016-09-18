using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factory;
using log4net;

namespace Dao.Base
{

    public abstract class BaseDao
    {
        protected RepositoryFactory RepositoryFactory { get; private set; }
        protected ILog Log { get; private set; }
        private Stopwatch _watch;

        protected Stopwatch Watch
        {
            get { return _watch ?? (_watch = new Stopwatch()); }
        }

        protected BaseDao()
        {
            RepositoryFactory = new RepositoryFactory();
            Log = LogManager.GetLogger(this.GetType().Name);
        }
    }

}
