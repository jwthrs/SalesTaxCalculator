using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesTaxCalculator.Models
{
    public class StateSalesTax 
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string TaxRate {get; set;}
        public List<CountyTax> countyTaxes { get; set; }
    }

    public class CountyTax
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public string TaxRate { get; set; }
    }
}
