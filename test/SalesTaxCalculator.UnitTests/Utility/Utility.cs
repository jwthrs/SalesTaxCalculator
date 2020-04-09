using System.Collections.Generic;
using SalesTaxCalculator.Models;
using SalesTaxCalculator.Builders;

namespace SalesTaxCalculator.UnitTests.Utility
{
    public class Utility
    {
        public static StateSalesTax CreateStateSalesTax(int stateId, string stateName, string stateTaxRate,
            List<CountyTax> countyTaxes)
        {
            return new StateSalesTax
            {
                Id = stateId,
                Name = stateName,
                TaxRate = stateTaxRate,
                CountyTaxes = countyTaxes
            };
        }

        public static CountyTax CreateCountyTax(int countyId, string countyName, string countyTaxRate)
        {
            return new CountyTax
            {
                Id = countyId,
                Name = countyName,
                TaxRate = countyTaxRate
            };
        }

        public static SalesTaxRequest CreateRequest(string stateName, string countyName, float itemPrice)
        {
            return new SalesTaxRequest()
            {
                State = stateName,
                County = countyName,
                ItemPrice = itemPrice
            };
        }

        public static SalesTaxResponse CreateResponse(string stateName, string countyName, float stateTax,
            float localTax, float totalTax)
        {
            return new SalesTaxResponse()
            {
                State = stateName,
                County = countyName,
                StateTax = stateTax,
                LocalTax = localTax,
                TotalTax = totalTax
            };
        }
        
        
    }
}