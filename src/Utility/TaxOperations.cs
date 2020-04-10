using System;

namespace SalesTaxCalculator.Utility
{
    /// <summary>
    /// TaxOperations contains functions that perform mathematical operations.
    /// </summary>
    public static class TaxOperations
    {

        /// <summary>
        /// Calculates the sales tax given the item price and the tax rate.
        /// 
        /// </summary>
        /// <param name="itemPrice">Price of the item.</param>
        /// <param name="taxRate">Sales tax rate.</param>
        /// <returns>A product of itemPrice and taxRate rounded to two decimal places.</returns>
        public static float CalculateSalesTax(float itemPrice, float taxRate)
        {
            return (float) Math.Round((double) itemPrice * (taxRate / 100));
        }
    }
}