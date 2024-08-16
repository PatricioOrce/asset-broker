using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils
{
    public class Response
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
        public string Message { get; set; }
        public string[] Errors { get; set; }

        public void SetSuccessResponse(string message)
        {
            this.Message = message;
        }
    }
    public class Response<T> : Response
    {
        public T Body { get; set; }
    }
}
