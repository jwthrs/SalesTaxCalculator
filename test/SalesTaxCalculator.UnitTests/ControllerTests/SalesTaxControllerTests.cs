﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SalesTaxCalculator.Context;
using SalesTaxCalculator.Services;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CoreUtilities;
using SalesTaxCalculator.Builders;
using SalesTaxCalculator.Models;
using SalesTaxCalculator.UnitTests.Utility;
using SalesTaxCalculator.Constants;
using SalesTaxCalculator.Utility;

namespace SalesTaxCaulcator.UnitTests.ControllerTests
{
	[TestClass]
	public class SalesTaxControllerTests
	{

		private Mock<ISalesTaxContext> _mockContext;
		
		public SalesTaxControllerTests()
		{
			_mockContext = new Mock<ISalesTaxContext>();
		}

		/// <summary>
		/// Test for error message when no state is provided in the request.
		/// 
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestNoState()
		{
			
		}

		/// <summary>
		/// Test for error message when no county is provided in the request.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestNoCounty()
		{
			
		}

		/// <summary>
		/// Test for error message when no item price is provided in the request.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestNoItemPrice()
		{
			
		}

		/// <summary>
		/// Test for error message when no state or county is provided in the request.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestNoStateNoCounty()
		{
			
		}
		
		/// <summary>
		/// Test for error message when no state or item price is provided in the request.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestNoStateNoItemPrice()
		{
			
		}

		/// <summary>
		/// Test for error message when no county or item price is provided in the request.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestNoCountyNoItemPrice()
		{
			
		}

		/// <summary>
		/// Test for error message when no state, county or item price is provided in the request.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestNoStateNoCountyNoItemPrice()
		{
			
		}

		/// <summary>
		/// Test to get invalid state error when all request data is valid except for state.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestBadState()
		{

		}

		/// <summary>
		/// Boundary test to get invalid item price error when all request data is valid except for item price.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestItemPriceBelow()
		{

		}

		/// <summary>
		/// Test to get invalid item price error when all request data is valid except for item price.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestBadCounty()
		{
			var testRequest = new SalesTaxRequest
			{
				State = "NoState",
				County = "BadCounty",
				ItemPrice = 19.99f
			};
			
			// Setup mock db context
			_mockContext.Setup(m => m.RetrieveState(testRequest.State)).ReturnsAsync(
				new StateSalesTax
				{
					Id = 1,
					Name = testRequest.State,
					TaxRate = "1.0",
					CountyTaxes = new List<CountyTax>
					{
						new CountyTax
						{
							Id = 1,
							Name = "NoCounty",
							TaxRate = "1.0"
						}
					}
				});

			var mediator = new SalesTaxMediator(_mockContext.Object);
			
			// Test mediator
			var result = (await mediator.CalculateSalesTaxAsync(testRequest) as BadRequestObjectResult)?.Value as ResponseError;
			
			// Assert if result is same as expected 
			Assert.AreEqual(String.Format(ErrorMessages.ErrCountyNotExistInState, "BadCounty","NoState"), result?.Error as string);
		}

		/// <summary>
		/// Test to get all invalid field errors when all request data is invalid for state, county, and item price.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequest()
		{
			
		}

		/// <summary>
		/// Test for valid response given valid data.
		/// </summary>
		/// <returns></returns>
		[TestMethod] 
		public async Task TestValidRequest()
		{

			var testRequest = new SalesTaxRequest
			{
				State = "NoState",
				County = "NoCounty",
				ItemPrice = 19.99f
			};

			var expectedResponse = new SalesTaxResponse
			{
				State = testRequest.State,
				County = testRequest.County,
				StateTax = 0.2f,
				LocalTax = 0.2f,
				TotalTax = 0.4f
			};
			
			// Setup mock db context
			_mockContext.Setup(m => m.RetrieveState(testRequest.State)).ReturnsAsync(
				new StateSalesTax
				{
					Id = 1,
					Name = testRequest.State,
					TaxRate = "1.0",
					CountyTaxes = new List<CountyTax>
					{
						new CountyTax
						{
							Id = 1,
							Name = "NoCounty",
							TaxRate = "1.0"
						}
					}
				});

			var mediator = new SalesTaxMediator(_mockContext.Object);
			
			// Test mediator
			var result = (await mediator.CalculateSalesTaxAsync(testRequest) as OkObjectResult)?.Value as SalesTaxResponse;
			
			// Assert if result is same as expected 
			Assert.AreEqual(expectedResponse.State, result?.State);
			Assert.AreEqual(expectedResponse.County, result?.County);
			Assert.AreEqual(expectedResponse.LocalTax, result?.LocalTax);
			Assert.AreEqual(expectedResponse.StateTax, result?.StateTax);
			Assert.AreEqual(expectedResponse.TotalTax, result?.TotalTax);
		}
	}
}
