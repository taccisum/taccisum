using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Generic;

namespace Repository.Repository.Base
{

    public abstract class BaseRepository
    {
        protected RepositoryFactory RepositoryFactory { get; private set; }


        protected BaseRepository()
        {
            RepositoryFactory = new RepositoryFactory();
        }
    }
}
