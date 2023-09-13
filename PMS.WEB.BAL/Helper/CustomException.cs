using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Helper
{
    public class CustomException:Exception
    {
        private readonly string _errorCode;
        private readonly IConfiguration _configuration;
        public CustomException()
        {

        }
        public CustomException(string errorCode, IConfiguration configuration)
        {
            _errorCode = errorCode;
            _configuration = configuration;
        }
        public JsonError GetErrorObject()
        {
            return new JsonError()
            {
                Status = _configuration.GetSection(_errorCode + ":" + "status").Value,
                Success = Convert.ToBoolean(_configuration.GetSection(_errorCode + ":" + "success").Value),
                Error = new CustomError
                {
                    Code = _configuration.GetSection(_errorCode + ":" + "code").Value,
                    Title = _configuration.GetSection(_errorCode + ":" + "title").Value,
                    Detail = _configuration.GetSection(_errorCode + ":" + "detail").Value,
                    DocUrl = _configuration.GetSection(_errorCode + ":" + "docURL").Value,
                    TraceId = _configuration.GetSection(_errorCode + ":" + "traceID").Value
                }
            };
        }
    }
    public enum ErrorCodes
    {
        GE001,
        GE002,
        GE003,
        GE004,
        GE005,
        GE006,        
        Default
    }
}
