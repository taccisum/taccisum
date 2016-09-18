using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;

namespace Dao.Interf
{
    public interface ISysMenuDao
    {
        IQueryable<SysMenu> Get(Expression<Func<SysMenu, bool>> expr);
        void Add(SysMenu menu);

        void Update(SysMenu menu);

        void Remove(Guid id);
    }
}
