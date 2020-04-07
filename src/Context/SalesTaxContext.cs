using System;
using Microsoft.EntityFrameworkCore;
using SalesTaxCalculator.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SalesTaxCalculator.Context
{
	public class SalesTaxContext : DbContext, ISalesTaxContext
	{

		public DbSet<StateSalesTax> stateSalesTaxes { get; set; }

		public SalesTaxContext(DbContextOptions<SalesTaxContext> options) : base(options)
		{
		
		}

		public async Task AddState(StateSalesTax model) {
			await stateSalesTaxes.AddAsync(model);
			await SaveChangesAsync();
		}


		public async Task<StateSalesTax> RetrieveState(string name) {
			return await stateSalesTaxes.Include(state => state.countyTaxes).FirstOrDefaultAsync(state => state.Name == name);
		}

	}

}
