namespace SalesTaxCalculator.Constants
{
    public static class ErrorMessages
    {
        public const string ERR_STATE_REQUIRED = "State is required.";
        public const string ERR_COUNTY_REQUIRED = "County is required.";
        public const string ERR_ITEMPRICE_REQUIRED = "Item price is required.";
        public const string ERR_ITEMPRICE_BOUNDARY = "Value for {0} should not be less than {1}, and not more than {2}.";
    }
}