using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;

namespace Service.Interf.Sys
{
    public interface ISysUserDemoService
    {

        SysUserDemo Create(SysUserDemo entity);
        void Delete(Guid id);

        IQueryable<SysUserDemo> GetAll();

        SysUserDemo Verify(string uid, string psd);
    }
}
