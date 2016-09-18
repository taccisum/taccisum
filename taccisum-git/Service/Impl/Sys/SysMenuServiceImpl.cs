using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using Common.Tool.Extend;
using Model.Models;
using Repository.Dao.Impl.Sys;
using Repository.Dao.Interf.Sys;
using Service.Base;
using Service.Interf.Sys;

namespace Service.Impl.Sys
{
    [Export(typeof(ISysMenuService))]
    public class SysMenuServiceImpl : BaseService, ISysMenuService
    {
        [Import]
        protected ISysMenuDao MenuDao { get; set; }

        public IQueryable<Model.Entity.SysMenu> Query()
        {
            return MenuDao.Query();
        }

        public IQueryable<Model.Entity.SysMenu> Query(Expression<Func<Model.Entity.SysMenu, bool>> expr)
        {
            return MenuDao.Query(expr);
        }

        public void Insert(Model.Entity.SysMenu menu)
        {
            MenuDao.Create(menu);
        }

        public void Update(Model.Entity.SysMenu menu)
        {
            MenuDao.Update(menu);
        }

        public void Delete(Guid id)
        {
            MenuDao.Delete(id);
        }

        public void Delete(IEnumerable<Guid> idList)
        {
            foreach (var id in idList)
            {
                MenuDao.Delete(id, false);
            }
            MenuDao.Submit();
        }

        public void Disable(Guid id)
        {
            var entity = MenuDao.GetEntity(id);
            entity.EnabledState = false;
            MenuDao.Update(entity);
        }

        public void Disable(IEnumerable<Guid> idList)
        {
            foreach (var id in idList)
            {
                var entity = MenuDao.GetEntity(id);
                entity.EnabledState = false;
                MenuDao.Update(entity, false);
            }
            MenuDao.Submit();
        }

        public object GetPageList(MenuQuery query)
        {
            var filterFlag = new
            {
                hasName = query.Name.IsNullOrWhiteSpace()
            };

            var menus = MenuDao.Query();

            var list = from m in menus
                where filterFlag.hasName && m.Name.Contains(query.Name)
                join m1 in menus on m.ParentId equals m1.ID into temp
                from t in temp.DefaultIfEmpty()
                select new
                {
                    ID = m.ID,
                    Name = m.Name,
                    Url = m.Url,
                    ParentId = m.ParentId,
                    ParentName = t.Name,
                    SortNo = m.SortNo,
                    Icon = m.Icon,
                    Description = m.Description,
                    EnabledState = m.EnabledState,
                    CreatedOn = m.CreatedOn
                };
            return list;
        }
    }
}
