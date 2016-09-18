using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dao.Base;
using Dao.Interf;
using Model.Entity;

namespace Dao.Impl
{
    public class SysMenuDaoImpl : DaoSupport<SysMenu>, ISysMenuDao
    {
        public IQueryable<SysMenu> Get()
        {
            return Query();
        }

        public IQueryable<SysMenu> Get(Expression<Func<SysMenu, bool>> expr)
        {
            return Query(expr);
        }

        public void Add(SysMenu menu)
        {
            base.Create(menu);
        }

        public void Update(SysMenu menu)
        {
            base.Update(menu);
        }

        public void Remove(Guid id)
        {
            base.Delete(id);
        }

        public void Remove(IEnumerable<Guid> idList)
        {
            foreach (var id in idList)
            {
                base.Delete(id, submit: false);
            }

            Submit();
        }

        public void Disable(Guid id)
        {
            var entity = GetEntity(id);
            entity.EnabledState = false;
            base.Update(entity);
        }

        public void Disable(IEnumerable<Guid> idList)
        {
            foreach (var id in idList)
            {
                var entity = GetEntity(id);
                entity.EnabledState = false;
                base.Update(entity, false);
            }
            Submit();
        }
    }
}
