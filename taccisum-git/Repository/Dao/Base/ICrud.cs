using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Base
{
    /// <summary>
    /// 实现增删改查接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICrud<T>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="isDelete">是否包含逻辑删除的记录</param>
        /// <returns></returns>
        IQueryable<T> Query(bool isDelete = false);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="expr">查询表达式</param>
        /// <param name="isDelete">是否包含逻辑删除的记录</param>
        /// <returns></returns>
        IQueryable<T> Query(Expression<Func<T, bool>> expr, bool isDelete = false);
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="submit">是否提交事务</param>
        T Create(T entity, bool submit = true);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="submit">是否提交事务</param>
        void Update(T entity, bool submit = true);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isLogic">是否逻辑删除</param>
        /// <param name="submit">是否提交事务</param>
        void Delete(T entity, bool isLogic = true, bool submit = true);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="isLogic">是否逻辑删除</param>
        /// <param name="submit">是否提交事务</param>
        void Delete(object primaryKey, bool isLogic = true, bool submit = true);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="primaryKey">主键键值</param>
        /// <returns></returns>
        T GetEntity(object primaryKey);
        /// <summary>
        /// 提交事务
        /// </summary>
        int Submit();
    }
}
