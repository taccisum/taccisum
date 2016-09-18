using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
    public class ApiResult
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }


        /// <summary>
        /// 生成一个成功的Result，返回信息默认为空
        /// </summary>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ApiResult SuccessResult(object data, string msg = "")
        {
            return new ApiResult()
            {
                Success = true,
                Data = data,
                Message = msg
            };
        }

        /// <summary>
        /// 生成一个失败的Result，返回数据默认为null
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResult FailedResult(string msg, object data = null)
        {
            return new ApiResult()
            {
                Success = false,
                Data = data,
                Message = msg
            };
        }
    }
}
