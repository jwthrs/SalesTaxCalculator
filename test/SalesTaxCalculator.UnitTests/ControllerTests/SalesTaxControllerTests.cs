using System;
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
using SalesTaxCalculator.Models;
using SalesTaxCalculator.Constants;

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
		/// Test to get invalid state error when all request data is valid except for state.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestBadState()
		{
			var testRequest = new SalesTaxRequest
			{
				State = "BadState",
				County = "NoCounty",
				ItemPrice = 19.99f
			};
			
			// Setup mock db context
			_mockContext.Setup(m => m.RetrieveState("NoState")).ReturnsAsync(
				new StateSalesTax
				{
					Id = 1,
					Name = "NoState",
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
			Assert.AreEqual(String.Format(ErrorMessages.ErrNotSupported, testRequest.State), result?.Error as string);
			
			_mockContext.Verify(m => m.RetrieveState(testRequest.State), Times.Once);
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
			
			_mockContext.Verify(m => m.RetrieveState(testRequest.State), Times.Once);
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
			
			_mockContext.Verify(m => m.RetrieveState(testRequest.State), Times.Once);
		}
	}
}
