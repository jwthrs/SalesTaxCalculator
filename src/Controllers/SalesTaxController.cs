using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using SalesTaxCalculator.Models;
using SalesTaxCalculator.Filters;
using SalesTaxCalculator.Services;

namespace SalesTaxCalculator.Controllers
{

    public class SalesTaxController : ControllerBase
    {
        private IConfiguration _config;
        private ISalesTaxMediator _mediator;

        public SalesTaxController(IConfiguration config, ISalesTaxMediator mediator)
        {
            _config = config;
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [TypeFilter(typeof(ModelStateValidationFilter))]
        public async Task<IActionResult> Calculate([FromBody]SalesTaxRequest request)
        {

            return await _mediator.CalculateSalesTaxAsync(request);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]StateSalesTax model) {
            await _mediator.AddAsync(model);
            return new OkResult();
        }
    }
}
