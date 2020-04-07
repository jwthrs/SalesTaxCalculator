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

		[TestMethod] 
		public async Task TestValidRequest()
		{
			var stateTaxObj = new StateSalesTax
			{
				Id = 1,
				Name = "NOBODY",
				TaxRate = "0",
				CountyTaxes = new List<CountyTax>
				{
					new CountyTax
					{
						Id = 1,
						Name = "NoCounty",
						TaxRate = "0"
					}
				}
			}; 
			
			_mockContext.Setup(m => m.RetrieveState(stateTaxObj.Name)).ReturnsAsync(stateTaxObj);
			var mediator = new SalesTaxMediator(_mockContext.Object);

			var result = await mediator.CalculateSalesTaxAsync(new SalesTaxRequest
			{
				State = stateTaxObj.Name,
				County = stateTaxObj.CountyTaxes.First().Name,
				ItemPrice = "19.99"
			});
			
			(result as ViewResult)?.Model.Should().BeEquivalentTo(new SalesTaxResponse
			{
				State = stateTaxObj.Name,
				County = stateTaxObj.CountyTaxes.First().Name,
				StateTax = 0,
				LocalTax = 0,
				TotalTax = 0
			});
			
			// Throw exception if never called
			_mockContext.Verify(m => m.RetrieveState(stateTaxObj.Name), Times.Once());
		}
	}
}
