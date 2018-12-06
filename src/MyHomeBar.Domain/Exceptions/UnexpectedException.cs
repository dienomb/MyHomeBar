namespace MyHomeBar.Domain.Exceptions
{
    using System;
    using System.Net;

    [Serializable]
    public class UnexpectedException : CustomStatusException
    {
        public UnexpectedException(string message, Exception innException = null)
            : base(message, HttpStatusCode.BadGateway, nameof(UnexpectedException), innException)
        {
        }
    }

}
