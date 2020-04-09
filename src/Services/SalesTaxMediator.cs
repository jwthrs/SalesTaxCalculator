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
    /// <summary>
    /// SalesTaxMediator acts as a mediator to the SalesTaxContext.
    /// It contains specific functionality to perform certain operations, such as calculating sales tax on an item.
    /// 
    /// </summary>
    public class SalesTaxMediator : ISalesTaxMediator
    {
        private ISalesTaxContext _context;

        /// <summary>
        /// Default constructor
        /// 
        /// </summary>
        /// <param name="context">Database context to interact with the SalesTax tables.</param>
        public SalesTaxMediator(ISalesTaxContext context) {
            _context = context;
        }

        /// <summary>
        /// Given the price of an item, and the state and county that it's sold in, calculates the local, state and total sales tax on that item.
        /// 
        /// </summary>
        /// <param name="request">Accepts a SalesTaxRequest object.</param>
        /// <returns>IActionResult that wraps the SalesTaxResponse if successful, bad request errors if the state or county doesn't exist or if item price is invalid.</returns>
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

            if (request.ItemPrice < 0.01)
            {
                return BadRequestError($"{request.ItemPrice} should be a dollar figure that is not equal to or below 0.00.");
            }

            var response = new SalesTaxResponse
            {
                State = request.State,
                County = request.County,
                StateTax = CalculateTax(request.ItemPrice, matchedState.TaxRate),
                LocalTax = CalculateTax(request.ItemPrice, matchedCounty.TaxRate)

            };

            response.TotalTax = response.StateTax + response.LocalTax;

            return new OkObjectResult(response);
        }

        /// <summary>
        /// Calculates the sales tax given the item price and the tax rate.
        /// 
        /// </summary>
        /// <param name="itemPrice">Price of the item.</param>
        /// <param name="taxRate">Sales tax rate.</param>
        /// <returns>A product of itemPrice and taxRate rounded to two decimal places.</returns>
        private float CalculateTax(float itemPrice, string taxRate)
        {
            return (float)Math.Round((double)itemPrice * (float.Parse(taxRate)/100), 2);
        }

        /// <summary>
        /// Adds a state to the database.
        /// 
        /// </summary>
        /// <param name="model">StateSalesTax model to add to the database.</param>
        /// <returns></returns>
        public async Task AddAsync(StateSalesTax model) {
            // TODO: Best if model is verified before it's added.
            await _context.AddState(model);
        }

    }
}
