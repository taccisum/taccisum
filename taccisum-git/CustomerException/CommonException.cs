using System;

namespace Common.CustomerException
{
    public class CommonException : ApplicationException
    {
        public string ErrorMsg { get; private set; }
        public object Data { get; private set; }

        public CommonException(string msg, object data = null)
        {
            ErrorMsg = msg;
            Data = data;
        }
    }
}
