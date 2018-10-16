using System;
using System.Net;

namespace RatesExchangeApi
{
    public class BaseException : Exception
    {
        protected BaseException(string message) : base(message) { }

        protected string Code { get; set; }

        protected string StatusCode { get; set; }
    }

    public class ValidationException : BaseException
    {
        public ValidationException(string code, string message) : base(message)
        {
            Code = code;
            StatusCode = HttpStatusCode.BadRequest.ToString();
        }
    }
}
