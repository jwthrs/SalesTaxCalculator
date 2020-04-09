namespace SalesTaxCalculator.Constants
{
    public static class ErrorMessages
    {
        public const string ErrStateRequired = "State is required.";
        public const string ErrCountyRequired = "County is required.";
        public const string ErrItempriceRequired = "Item price is required.";
        public const string ErrItempriceBoundary = "Value for {0} should not be less than {1}, and not more than {2}.";
    }
}