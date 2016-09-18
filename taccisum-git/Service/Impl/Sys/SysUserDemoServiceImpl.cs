using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Global.Enum.Common;
using Model.Entity;
using Repository.Dao.Interf.Sys;
using Service.Base;
using Service.Interf.Sys;

namespace Service.Impl.Sys
{
    [Export(typeof(ISysUserDemoService))]
    public class SysUserDemoServiceImpl: BaseService, ISysUserDemoService
    {
        [Import]
        protected ISysUserDemoDao SysUserDemoDao { get; set; }

        public SysUserDemo Create(SysUserDemo entity)
        {
            return SysUserDemoDao.Create(entity);
        }

        public void Delete(Guid id)
        {
            SysUserDemoDao.Delete(id);
        }

        public IQueryable<SysUserDemo> GetAll()
        {
            return SysUserDemoDao.Query();
        }

        public SysUserDemo Verify(string uid, string psd)
        {
            return SysUserDemoDao.LoginVerify(uid, psd, EncryptType.Unencrypted);
        }
    }
}
