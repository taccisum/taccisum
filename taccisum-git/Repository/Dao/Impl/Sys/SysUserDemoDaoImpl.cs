using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Global.Enum.Common;
using Common.Tool.Extend;
using Model.Entity;
using Repository.Dao.Interf.Sys;
using Repository.Repository.Base;

namespace Repository.Dao.Impl.Sys
{
    [Export(typeof(ISysUserDemoDao))]
    public class SysUserDemoDaoImpl : RepositorySupport<SysUserDemo>, ISysUserDemoDao
    {
        public SysUserDemo LoginVerify(string uid, string psd, EncryptType encryptType)
        {
            var encrypt = EncryptFactory.GetInstance().Create(encryptType);
            var encryptPsd = encrypt.Encrypt(psd);

            var user = Query(u => u.Account == uid).FirstOrDefault();
            if (user == null || user.Password != encryptPsd)
                return null;
            return user;
        }

        #region private
        private class EncryptFactory
        {
            private static EncryptFactory _instance;

            public static EncryptFactory GetInstance()
            {
                if (_instance == null)
                    _instance = new EncryptFactory();
                return _instance;
            }


            public IEncrypt Create(EncryptType type)
            {
                switch (type)
                {
                    case EncryptType.Unencrypted:
                        return new Unencrypted();
                    case EncryptType.MD5_32:
                        return new MD5_32();
                    case EncryptType.MD5_64:
                        return new MD5_64();
                    case EncryptType.DES:
                        return new DES();
                    default:
                        return new Unencrypted();
                }
            }

        }

        private interface IEncrypt
        {
            string Encrypt(string psd);
        }

        #region encrypt method
        private class Unencrypted : IEncrypt
        {
            public string Encrypt(string psd)
            {
                return psd;
            }
        }

        private class MD5_32 : IEncrypt
        {
            public string Encrypt(string psd)
            {
                return psd.ToMD5();
            }
        }

        private class MD5_64 : IEncrypt
        {
            public string Encrypt(string psd)
            {
                throw new NotImplementedException();
            }
        }

        private class DES : IEncrypt
        {
            public string Encrypt(string psd)
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #endregion
    }
}
