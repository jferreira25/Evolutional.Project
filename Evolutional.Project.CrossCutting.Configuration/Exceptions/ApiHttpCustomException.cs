using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Evolutional.Project.CrossCutting.Configuration.Exceptions
{
    public class ApiHttpCustomException : Exception
    {
        public HttpStatusCode ResponseCode { get; }

       
        public ApiHttpCustomException(string message, HttpStatusCode responseCode) : base(message)
        {
            ResponseCode = responseCode;
        }

        public ApiHttpCustomException(string message, HttpStatusCode responseCode, Exception innerException) : base(message, innerException)
        {
            ResponseCode = responseCode;
        }
    }
}