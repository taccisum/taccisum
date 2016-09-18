using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;

namespace Repository.Repository.Base
{
    /// <summary>
    /// 此基类的所有方法均直接针对类型T所映射的数据表进行CRUD
    /// 如需在子类中操作其它表，请使用BaseRepository中的Repository直接进行操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositorySupport<T> : BaseRepository, ICrud<T> where T : DTO, new()
    {
        public virtual IQueryable<T> Query(bool isDelete = false)
        {
            return Query(t => true, isDelete);
        }

        public virtual IQueryable<T> Query(Expression<Func<T, bool>> expr, bool isDelete = false)
        {
            return RepositoryFactory.At<T>().Get(expr, isDelete);
        }

        public virtual T Create(T entity, bool submit = true)
        {
            var r = RepositoryFactory.At<T>().Insert(entity);
            if (submit)
                return Submit() != -1 ? r : null;
            return r;
        }

        public virtual void Update(T entity, bool submit = true)
        {
            RepositoryFactory.At<T>().Update(entity);
            if (submit)
                Submit();
        }

        public virtual void Delete(T entity, bool isLogic = true, bool submit = true)
        {
            RepositoryFactory.At<T>().Delete(entity, isLogic);
            if (submit)
                Submit();
        }

        public void Delete(object primaryKey, bool isLogic = true, bool submit = true)
        {
            RepositoryFactory.At<T>().Delete(primaryKey, isLogic);
            if (submit)
                Submit();
        }

        public virtual T GetEntity(object primaryKey)
        {
            return RepositoryFactory.At<T>().GetEntryByPrimaryKey(primaryKey);
        }

        public virtual int Submit()
        {
            return RepositoryFactory.Submit();
        }

    }
}
