using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SalesTaxCalculator.Constants;

namespace SalesTaxCalculator.Models
{
    /// <summary>
    /// StateSalesTax is an Entity Framework implementation of the SQL StateSalesTax table.
    /// Each StateSalesTax has a name for the state and a state-wide tax-rate, and a list of contained counties with their own names and tax-rates.
    /// </summary>
    public class StateSalesTax 
    {
        public int Id {get; set;}
        [Required(ErrorMessage = ErrorMessages.ErrStateRequired)]
        public string Name {get; set;}
        [Required(ErrorMessage = ErrorMessages.ErrTaxRateRequired)]
        public string TaxRate {get; set;}
        public List<CountyTax> CountyTaxes { get; set; }
    }

    /// <summary>
    /// CountyTax is an Entity Framework implementation of the SQL CountyTax table.
    /// Each CountyTax has a name for the county and a county-wide tax-rate.
    /// </summary>
    public class CountyTax
    {
        public int Id {get; set;}
        [Required(ErrorMessage = ErrorMessages.ErrCountyRequired)]
        public string Name { get; set; }
        [Required(ErrorMessage = ErrorMessages.ErrTaxRateRequired)]
        public string TaxRate { get; set; }
    }
}
