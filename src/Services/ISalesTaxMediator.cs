using Microsoft.AspNetCore.Mvc;
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
