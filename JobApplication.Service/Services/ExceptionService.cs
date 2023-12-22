using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.Services
{
    public class ExceptionService : Exception
    {
        public ExceptionService(int errorCode, params object[] data) : base(data[0].ToString())
        {
            ErrorCode = errorCode;
            ErrorData = data;
        }
        public ExceptionService(int errorCode) : base(errorCode.ToString())
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; }
        public object[] ErrorData { get; }
    }
}
