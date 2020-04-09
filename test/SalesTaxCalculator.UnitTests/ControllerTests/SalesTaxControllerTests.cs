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
		/// Test to get invalid state error when all request data is valid except for state.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestState()
		{
			var dummyCountyTax = Utility.CreateCountyTax(1, "NoCounty", "0.01");
			var dummyStateTax = Utility.CreateStateSalesTax(1, "NoState", "0.01", new List<CountyTax> {dummyCountyTax});
			
			var dummyBadStateName = "BadState";
			var testRequest = Utility.CreateRequest(dummyBadStateName, dummyStateTax.CountyTaxes.First().Name, 19.99f);

			/*
			// TODO: Create error response.
			var expectedResponse;
			*/
			_mockContext.Setup(m => m.RetrieveState(dummyStateTax.Name)).ReturnsAsync(dummyStateTax);
			var mediator = new SalesTaxMediator(_mockContext.Object);
		}

		/// <summary>
		/// Test to get invalid county error when all request data is valid except for county.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestCounty()
		{
			var testCountyTax = Utility.CreateCountyTax(1, "NoCounty", "0.01");
			var testStateTax = Utility.CreateStateSalesTax(1, "NoState", "0.01", new List<CountyTax> {testCountyTax});
			var testRequest = Utility.CreateRequest(testStateTax.Name, testStateTax.CountyTaxes.First().Name, 19.99f);

			_mockContext.Setup(m => m.RetrieveState(testStateTax.Name)).ReturnsAsync(testStateTax);
			var mediator = new SalesTaxMediator(_mockContext.Object);
		}

		/// <summary>
		/// Boundary test to get invalid item price error when all request data is valid except for item price.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestItemPriceBelow()
		{
			var testCountyTax = Utility.CreateCountyTax(1, "NoCounty", "0.01");
			var testStateTax = Utility.CreateStateSalesTax(1, "NoState", "0.01", new List<CountyTax> {testCountyTax});
			var testRequest = Utility.CreateRequest(testStateTax.Name, testStateTax.CountyTaxes.First().Name, 19.99f);

			_mockContext.Setup(m => m.RetrieveState(testStateTax.Name)).ReturnsAsync(testStateTax);
			var mediator = new SalesTaxMediator(_mockContext.Object);
		}

		/// <summary>
		/// Test to get invalid item price error when all request data is valid except for item price.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequestItemPrice()
		{
			var testCountyTax = Utility.CreateCountyTax(1, "NoCounty", "0.01");
			var testStateTax = Utility.CreateStateSalesTax(1, "NoState", "0.01", new List<CountyTax> {testCountyTax});
			var testRequest = Utility.CreateRequest(testStateTax.Name, testStateTax.CountyTaxes.First().Name, 19.99f);

			_mockContext.Setup(m => m.RetrieveState(testStateTax.Name)).ReturnsAsync(testStateTax);
			var mediator = new SalesTaxMediator(_mockContext.Object);
		}

		/// <summary>
		/// Test to get all invalid field errors when all request data is invalid for state, county, and item price.
		/// </summary>
		/// <returns></returns>
		[TestMethod]
		public async Task TestInvalidRequest()
		{
			var testCountyTax = Utility.CreateCountyTax(1, "NoCounty", "0.01");
			var testStateTax = Utility.CreateStateSalesTax(1, "NoState", "0.01", new List<CountyTax> {testCountyTax});
			var testRequest = Utility.CreateRequest(testStateTax.Name, testStateTax.CountyTaxes.First().Name, 19.99f);

			_mockContext.Setup(m => m.RetrieveState(testStateTax.Name)).ReturnsAsync(testStateTax);
			var mediator = new SalesTaxMediator(_mockContext.Object);
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
