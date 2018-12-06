namespace MyHomeBar.Api.HttpErrors
{

    using Domain.Exceptions;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public class CustomHttpError
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public string CustomCode { get; set; }

        public string UserMessage { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> ValidationErrors { get; set; }

        public static CustomHttpError CreateHttpValidationError(
            HttpStatusCode status,
            string userMessage,
            IEnumerable<string> validationErrors = null)
        {
            return CreateDefaultHttpError(status, userMessage, status.ToString(), validationErrors);
        }

        public static CustomHttpError CreateCustomHttpError(Exception exception)
        {
            if (exception is CustomStatusException ex)
            {
                return CreateDefaultHttpError(ex.ErrorCode, exception.Message, ex.CustomErrorCode, ex.ErrorMessages);
            }
            return CreateDefaultHttpError(HttpStatusCode.InternalServerError, "Internal server error", "INTERNAL_SERVER_ERROR");
        }

        private static CustomHttpError CreateDefaultHttpError(
            HttpStatusCode status,
            string userMessage,
            string customcode = "",
            IEnumerable<string> validationErrors = null)
        {
            CustomHttpError httpError = new CustomHttpError
            {
                HttpStatusCode = status,
                UserMessage = userMessage,
                CustomCode = customcode,
            };

            if (validationErrors != null)
            {
                List<string> httpErrorValidationErrors = validationErrors.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                if (httpErrorValidationErrors.Any())
                {
                    httpError.ValidationErrors = httpErrorValidationErrors;
                }
            }
            return httpError;
        }
    }

}
