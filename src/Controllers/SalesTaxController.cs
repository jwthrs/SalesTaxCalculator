using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesTaxCalculator.Models;
using SalesTaxCalculator.Filters;
using SalesTaxCalculator.Services;

namespace SalesTaxCalculator.Controllers
{

    /// <summary>
    /// SalesTaxController provides endpoints to perform operations.
    /// </summary>
    public class SalesTaxController : ControllerBase
    {
        private ISalesTaxMediator _mediator;

        /// <summary>
        /// Constructor dependency injects a SalesTaxMediator.
        /// </summary>
        /// <param name="mediator"></param>
        public SalesTaxController(ISalesTaxMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Given the item price, state and county information, gives a response containing a sales tax breakdown by state, county, and total tax.
        /// 
        /// </summary>
        /// <param name="request">A body in the form of a SalesTaxRequest.</param>
        /// <returns>SalesTaxResponse that contains a sales tax breakdown by state, county, and total tax.</returns>
        [HttpPost("[action]")]
        [TypeFilter(typeof(ModelStateValidationFilter))]
        public async Task<IActionResult> Calculate([FromBody]SalesTaxRequest request)
        {

            return await _mediator.CalculateSalesTaxAsync(request);
        }

        /// <summary>
        /// Adds a State to the database.
        /// 
        /// </summary>
        /// <param name="model">StateSalesTax model that contains information to add to the database.</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]StateSalesTax model) {
            await _mediator.AddAsync(model);
            return new OkResult();
        }
    }
}
