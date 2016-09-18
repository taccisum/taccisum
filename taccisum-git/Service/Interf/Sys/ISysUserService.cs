using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Model.Models;

namespace Service.Interf.Sys
{
    public interface ISysUserService
    {
        /// <summary>
        /// 验证用户登陆
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        SysUser LoginVerify(SysUser info);
        /// <summary>
        /// 验证用户登陆
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="psd"></param>
        /// <returns></returns>
        SysUser LoginVerify(string uid, string psd);
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        SysUser Register(SysUser user);
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        SysUser Register(string uid, string psd);
        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysUser GetById(Guid id);
        /// <summary>
        /// 查询并返回用户列表
        /// </summary>
        /// <returns></returns>
        IQueryable<SysUser> GetAll();
    }

}
