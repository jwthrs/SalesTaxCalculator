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
using SalesTaxCalculator.Models;
using SalesTaxCalculator.UnitTests.Utility;
using SalesTaxCalculator.Constants;

namespace SalesTaxCaulcator.UnitTests.ControllerTests
{
	[TestClass]
	public class SalesTaxControllerTests
	{

		private Mock<SalesTaxContext> _mockContext;
		
		public SalesTaxControllerTests()
		{
			_mockContext = new Mock<SalesTaxContext>();
		}

		/// <summary>
		/// Test for error message when no state is provided in the request.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestNoState()
		{

			var expectedError = Utility.CreateErrorResponse(ErrorMessages.ErrStateRequired);
			
			

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
		/// Test to get invalid county error when all request data is valid except for county.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestBadCounty()
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
		public async Task TestInvalidRequestBadItemPrice()
		{

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
			// Set up testing parameters
			var dummyCountyTax = Utility.CreateCountyTax(1, "NoCounty", "0.01");
			var dummyStateTax = Utility.CreateStateSalesTax(1, "NoState", "0.01", new List<CountyTax> {dummyCountyTax});
			var testRequest = Utility.CreateRequest(dummyStateTax.Name, dummyStateTax.CountyTaxes.First().Name, 19.99f);
			var expectedResponse =
				Utility.CreateResponse(dummyStateTax.Name, dummyStateTax.CountyTaxes.First().Name, 0, 0, 0);
			
			// Setup mock context
			_mockContext.Setup(m => m.RetrieveState(dummyStateTax.Name)).ReturnsAsync(dummyStateTax);
			var mediator = new SalesTaxMediator(_mockContext.Object);
			
			// Test mediator
			var result = await mediator.CalculateSalesTaxAsync(testRequest);
			
			// Assert if result is same as expected response
			(result as ViewResult)?.Model.Should().BeEquivalentTo(expectedResponse);
			
			// Throw exception if never called
			_mockContext.Verify(m => m.RetrieveState(dummyStateTax.Name), Times.Once());
		}
	}
}
