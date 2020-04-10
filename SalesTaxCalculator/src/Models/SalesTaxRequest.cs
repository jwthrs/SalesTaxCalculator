using System.ComponentModel.DataAnnotations;
using SalesTaxCalculator.Constants;


namespace SalesTaxCalculator.Models
{

	/// <summary>
	/// SalesTaxRequest is a model of the expected body given to the "Calculate" endpoint.
	/// </summary>
	public class SalesTaxRequest
	{

		[Required(ErrorMessage = ErrorMessages.ErrStateRequired)]
		public string State { get; set; }

		[Required(ErrorMessage = ErrorMessages.ErrCountyRequired)]
		public string County { get; set; }

		[Required(ErrorMessage = ErrorMessages.ErrItempriceRequired)]
		[Range(0.01f, float.MaxValue, ErrorMessage = ErrorMessages.ErrItempriceBoundary)]
		public float ItemPrice { get; set; }
	}
}

