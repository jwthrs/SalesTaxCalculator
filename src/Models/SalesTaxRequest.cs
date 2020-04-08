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
		public string ItemPrice { get; set; }
	}
}

