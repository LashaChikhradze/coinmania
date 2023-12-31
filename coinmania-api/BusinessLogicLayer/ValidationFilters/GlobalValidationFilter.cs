using BusinessLogicLayer.ValidationErrors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BusinessLogicLayer.ValidationFilters;

public class GlobalValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                ?.SelectMany(o => o.Value.Errors.Select(e => new ErrorMessage()
                {
                    ErrorProducer = o.Key,
                    Message = e.ErrorMessage
                }));

            ErrorResponse response = new ErrorResponse() { Errors = errors };
            context.Result = new BadRequestObjectResult(response);
            return;
        }

        await next();
    }
}