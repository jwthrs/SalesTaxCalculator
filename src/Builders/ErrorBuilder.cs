using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesTaxCalculator.Builders
{
    /// <summary>
    /// ErrorBuilder contains functions that generate error messages.
    /// </summary>
    public static class ErrorBuilder {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorObject"></param>
        /// <returns></returns>
        public static IActionResult BadRequestError(object errorObject)
        {
            return new BadRequestObjectResult(new
            {
                Error = errorObject
            });
        }
    }
}
