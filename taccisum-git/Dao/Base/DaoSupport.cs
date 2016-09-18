using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Factory;
using Model.Entity;

namespace Dao.Base
{
    /// <summary>
    /// 此基类的所有方法均直接针对类型T所映射的数据表进行CRUD
    /// 如需在Dao中操作其它表，请使用Dao中的Repository直接进行操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DaoSupport<T> : BaseDao where T : DTO, new()
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="isDelete">是否包含逻辑删除的记录</param>
        /// <returns></returns>
        protected virtual IQueryable<T> Query(bool isDelete = false)
        {
            return Query(t => true, isDelete);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="expr">查询表达式</param>
        /// <param name="isDelete">是否包含逻辑删除的记录</param>
        /// <returns></returns>
        protected virtual IQueryable<T> Query(Expression<Func<T, bool>> expr, bool isDelete = false)
        {
            return RepositoryFactory.At<T>().Get(expr, isDelete);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="submit">是否提交事务</param>
        protected virtual void Create(T entity, bool submit = true)
        {
            RepositoryFactory.At<T>().Insert(entity);
            if (submit)
                Submit();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="submit">是否提交事务</param>
        protected virtual void Update(T entity, bool submit = true)
        {
            RepositoryFactory.At<T>().Update(entity);
            if (submit)
                Submit();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isLogic">是否逻辑删除</param>
        /// <param name="submit">是否提交事务</param>
        protected virtual void Delete(T entity, bool isLogic = true, bool submit = true)
        {
            RepositoryFactory.At<T>().Delete(entity, isLogic);
            if (submit)
                Submit();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="isLogic">是否逻辑删除</param>
        /// <param name="submit">是否提交事务</param>
        protected void Delete(object primaryKey, bool isLogic = true, bool submit = true)
        {
            RepositoryFactory.At<T>().Delete(primaryKey, isLogic);
            if (submit)
                Submit();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="primaryKey">主键键值</param>
        /// <returns></returns>
        protected virtual T GetEntity(object primaryKey)
        {
            return RepositoryFactory.At<T>().GetEntryByPrimaryKey(primaryKey);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        protected virtual void Submit()
        {
            RepositoryFactory.Submit();
        }
    }
}
