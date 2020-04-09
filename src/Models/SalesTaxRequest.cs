using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace SalesTaxCalculator.Models
{

	/// <summary>
	/// SalesTaxRequest is a model of the expected body given to the "Calculate" endpoint.
	/// </summary>
	public class SalesTaxRequest
	{
		[Required(ErrorMessage = "State is required.")]
		public string State { get; set; }

		[Required(ErrorMessage = "County is required.")]
		public string County { get; set; }

		[Required(ErrorMessage = "ItemPrice is required.")]
		[Range(0.01f, float.MaxValue, ErrorMessage = "Value for {0} should not be less than {1}, and not more than {2}")]
		public float ItemPrice { get; set; }
	}
}

