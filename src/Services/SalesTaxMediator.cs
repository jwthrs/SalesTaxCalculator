using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesTaxCalculator.Models;

namespace SalesTaxCalculator.Services
{
    public class SalesTaxMediator : ISalesTaxMediator
    {
        public Task<SalesTaxResponse> CalculateSalesTax(SalesTaxRequest request)
        {
            // More validation. Check if the state is real. But not here. Do in filter.
            // Get configuration into filter.
        }
    }
}
