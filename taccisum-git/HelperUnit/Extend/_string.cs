using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Tool.Extend
{
    public static class _string
    {
        /// <summary>
        /// 字符串格式的Guid 转 Guid对象，转换失败返回Guid.Empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                Guid g;
                if (Guid.TryParse(str, out g))
                {
                    return g;
                }
                else
                {
                    return Guid.Empty;
                }
            }
            else
            {
                return Guid.Empty;
            }
        }

        #region 根据自定义编码开头类型，返回一个编码开头+精确到秒当前时间+的编码
        public static string GetSequence(this string type)
        {
            string timeStr = DateTime.Now.ToString("yyyyMMddHHmmss");

            Random ran = new Random();
            int n = ran.Next(100, 999);

            return type+timeStr+n.ToString();
        }
        #endregion

        /// <summary>
        /// 对字符串进行MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMD5(this string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(str);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < md5data.Length; i++)
            {
                sBuilder.Append(md5data[i].ToString("X2"));
            }
            return sBuilder.ToS();
        }

        /// <summary>
        /// 对象转换为string，失败返回空字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToS(this object str)
        {
            if (str != null)
            {
                return str.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
