using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using SalesTaxCalculator.Constants;


namespace SalesTaxCalculator.Models
{

	/// <summary>
	/// SalesTaxRequest is a model of the expected body given to the "Calculate" endpoint.
	/// </summary>
	public class SalesTaxRequest
	{

		[Required(ErrorMessage = ErrorMessages.ERR_STATE_REQUIRED)]
		public string State { get; set; }

		[Required(ErrorMessage = ErrorMessages.ERR_COUNTY_REQUIRED)]
		public string County { get; set; }

		[Required(ErrorMessage = ErrorMessages.ERR_ITEMPRICE_REQUIRED)]
		[Range(0.01f, float.MaxValue, ErrorMessage = ErrorMessages.ERR_ITEMPRICE_BOUNDARY)]
		public float ItemPrice { get; set; }
	}
}

