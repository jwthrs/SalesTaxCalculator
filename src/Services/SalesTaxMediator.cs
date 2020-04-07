using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesTaxCalculator.Models;
using SalesTaxCalculator.Context;
using static SalesTaxCalculator.Builders.ErrorBuilder;

namespace SalesTaxCalculator.Services
{
    public class SalesTaxMediator : ISalesTaxMediator
    {
        private ISalesTaxContext _context;

        public SalesTaxMediator(ISalesTaxContext context) {
            _context = context;
        }

        public async Task<IActionResult> CalculateSalesTaxAsync(SalesTaxRequest request)
        {
            var matchedState = await _context.RetrieveState(request.State);

            if (matchedState == null) {
                // Didn't find State
                return BadRequestError($"{request.State} is not supported.");
            }

            var matchedCounty = matchedState.CountyTaxes?.FirstOrDefault(county => county.Name == request.County);
            if (matchedCounty == null)
            {
                // Didn't find County
                return BadRequestError($"{request.County} does not exist in {request.State}");
            }


            if (!float.TryParse(request.ItemPrice, out float itemPrice))
            {
                return BadRequestError($"{request.ItemPrice} couldn't be parsed to a float number value.");
            }

            var response = new SalesTaxResponse
            {
                State = request.State,
                County = request.County,
                StateTax = CalculateTax(itemPrice, matchedState.TaxRate),
                LocalTax = CalculateTax(itemPrice, matchedCounty.TaxRate)

            };

            response.TotalTax = response.StateTax + response.LocalTax;

            return new OkObjectResult(response);
        }

        private float CalculateTax(float itemPrice, string taxRate)
        {
            return itemPrice * (float.Parse(taxRate)/100);
        }

        

        public async Task AddAsync(StateSalesTax model) {
            await _context.AddState(model);
        }

    }
}
