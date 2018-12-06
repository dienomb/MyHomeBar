namespace MyHomeBar.Domain.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    public class CustomStatusException : Exception
    {
        public CustomStatusException(string message, HttpStatusCode errorCode, string customErrorCode, Exception exc = null)
            : base(message, exc)
        {
            this.ErrorCode = errorCode;
            this.CustomErrorCode = customErrorCode;
            this.ErrorMessages = new List<string>();
        }

        public HttpStatusCode ErrorCode { get; }

        public string CustomErrorCode { get; }

        public IEnumerable<string> ErrorMessages { get; }
    }

}
