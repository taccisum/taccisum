using System.ComponentModel.Composition;
using Repository.Dao.Interf.Sys;
using Repository.Repository.Base;

namespace Repository.Dao.Impl.Sys
{
    [Export(typeof(ISysMenuDao))]
    public class SysMenuDaoImpl : RepositorySupport<Model.Entity.SysMenu>, ISysMenuDao
    {
    }
}
