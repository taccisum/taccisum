using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Model.Models;

namespace Service.Interf.Sys
{
    public interface ISysMenuService
    {
        IQueryable<Model.Entity.SysMenu> Query();
        IQueryable<Model.Entity.SysMenu> Query(Expression<Func<Model.Entity.SysMenu, bool>> expr);
        void Insert(Model.Entity.SysMenu menu);

        void Update(Model.Entity.SysMenu menu);
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        void Delete(Guid id);
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="idList"></param>
        void Delete(IEnumerable<Guid> idList);
        /// <summary>
        /// 失能菜单
        /// </summary>
        /// <param name="id"></param>
        void Disable(Guid id);
        /// <summary>
        /// 失能菜单
        /// </summary>
        /// <param name="idList"></param>
        void Disable(IEnumerable<Guid> idList);
        #region Web
        /// <summary>
        /// 获取供web页面使用的菜单列表
        /// </summary>
        /// <param name="query">过滤条件</param>
        /// <returns></returns>
        object GetPageList(MenuQuery query);
        #endregion
    }
}
