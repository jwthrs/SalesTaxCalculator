using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace SalesTaxCalculator.Models
{

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

