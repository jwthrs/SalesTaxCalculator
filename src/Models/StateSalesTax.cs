using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesTaxCalculator.Models
{
    public class StateSalesTax : AreaTax
    {
        public List<AreaTax> countyTaxes { get; set; }
    }

    public class AreaTax
    {
        public string Name { get; set; }
        public string TaxRate { get; set; }
    }
}
