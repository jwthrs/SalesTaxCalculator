using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesTaxCalculator.Models;

namespace SalesTaxCalculator.Services
{
    public interface ISalesTaxMediator
    {
        Task<IActionResult> CalculateSalesTaxAsync(SalesTaxRequest request);
        Task AddAsync(StateSalesTax model);
    }
}
