﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Global.Enum.Common;
using Model.Entity;
using Repository.Repository.Base;

namespace Repository.Dao.Interf.Sys
{
    public interface ISysUserDao : ICrud<SysUser>
    {
        SysUser LoginVerify(string uid, string psd, EncryptType encryptType);
    }
}
