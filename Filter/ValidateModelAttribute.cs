using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BankApplication.Filter
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorDetails = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();

                var errorResponse = new
                {
                    Status = false,
                    Message = "Validation failed.",
                    Errors = errorDetails
                };

                //context.Result = new BadRequestObjectResult(errorResponse);
                context.Result = new JsonResult(errorResponse)
                {
                    StatusCode = 400
                };


            }
        }

    }
}
