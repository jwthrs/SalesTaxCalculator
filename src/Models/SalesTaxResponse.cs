using System;

namespace SalesTaxCalculator.Models
{
    /// <summary>
    /// SalesTaxResponse is a model of what the "Calculate" endpoint will return.
    /// </summary>
    public class SalesTaxResponse
    {
        public string State { get; set; }
        public string County { get; set; }
        public float StateTax { get; set; }
        public float LocalTax { get; set; }
        public float TotalTax { get; set; }
    }
}
