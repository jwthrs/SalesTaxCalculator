using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesTaxCalculator.Builders
{
    public static class ErrorBuilder {

        public static IActionResult BadRequestError(object errorObject)
        {
            return new BadRequestObjectResult(new
            {
                Error = errorObject
            });
        }
    }
}
