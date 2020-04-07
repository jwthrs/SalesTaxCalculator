using System;

namespace SalesTaxCalculator.Models
{
    public class SalesTaxResponse
    {
        public string State { get; set; }
        public string County { get; set; }
        public float StateTax { get; set; }
        public float LocalTax { get; set; }
        public float TotalTax { get; set; }
    }
}
