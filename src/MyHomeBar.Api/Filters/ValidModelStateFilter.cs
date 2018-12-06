namespace MyHomeBar.Api.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using MyHomeBar.Api.HttpErrors;
    using System.Linq;
    using System.Net;

    public class ValidModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            string[] validationErrors = context.ModelState
                .Keys
                .SelectMany(k => context.ModelState[k].Errors)
                .Select(e => e.ErrorMessage)
                .ToArray();

            CustomHttpError error = CustomHttpError.CreateHttpValidationError(
                HttpStatusCode.BadRequest,
                "There are validation errors",
                validationErrors);

            //if (error.ValidationErrors != null && error.ValidationErrors.Any())
            //{
            //    this.logger.LogError(error.ValidationErrors);
            //}

            context.Result = new BadRequestObjectResult(error);
        }
    }
}
