using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using static SalesTaxCalculator.Builders.ErrorBuilder;

namespace SalesTaxCalculator.Filters
{
    public class ModelStateValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // Catch multiple request errors if present. Returns 400.
                context.Result = BadRequestError(context.ModelState.Select(e => e.Value.Errors.FirstOrDefault().ErrorMessage));
            }
        }
    }
}