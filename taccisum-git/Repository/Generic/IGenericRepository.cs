using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Generic
{
    public interface IGenericRepository<T>
    {
        /// <summary>
        /// 获取集合的所有记录
        /// </summary>
        /// <param name="isDeleted">是否包括被逻辑删除的条目</param>
        /// <returns></returns>
        IQueryable<T> Get(bool isDeleted = false);
        /// <summary>
        /// 获取集合的记录
        /// </summary>
        /// <param name="expression">筛选条目的lambda表达式</param>
        /// <param name="isDeleted">是否包括被逻辑删除的条目</param>
        /// <returns></returns>
        IQueryable<T> Get(Expression<Func<T, bool>> expression, bool isDeleted = false);

        T FirstOrDefault(Expression<Func<T, bool>> expression, bool isDeleted = false);
        /// <summary>
        /// 通过主键获取记录
        /// </summary>
        /// <param name="primaryKey">键值</param>
        /// <returns></returns>
        T GetEntryByPrimaryKey(params object[] primaryKey);
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity"></param>
        T Insert(T entity);
        /// <summary>
        /// 根据实体删除指定记录
        /// </summary>
        /// <param name="entity">要删除的记录实体</param>
        /// <param name="isLogic">是否逻辑删除</param>
        void Delete(T entity, bool isLogic = true);
        /// <summary>
        /// 根据主键删除指定记录
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <param name="isLogic">是否逻辑删除</param>
        void Delete(object primaryKey, bool isLogic = true);
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// 提交变更
        /// </summary>
        /// <returns></returns>
        int Submit();
    }
}