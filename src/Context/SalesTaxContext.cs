using System;
using Microsoft.EntityFrameworkCore;
using SalesTaxCalculator.Models;


namespace SalesTaxCalculator.Context
{
	public class SalesTaxContext : DbContext, ISalesTaxContext
	{

		public DbSet<StateSalesTax> stateSalesTaxes { get; set; }

		public SalesTaxContext(DbContextOptions<SalesTaxContext> options) : base(options)
		{
		
		}
	}

}
