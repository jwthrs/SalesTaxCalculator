using System;
using Microsoft.EntityFrameworkCore;

public class SalesTaxContext : DbContext
{
	public SalesTaxContext(DbContextOptions<SalesTaxContext> options) : base(options)
	{

	}
}
