using System;
using Microsoft.EntityFrameworkCore;
using SalesTaxCalculator.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SalesTaxCalculator.Context
{
	
	/// <summary>
	/// Context for interacting with the database.
	/// </summary>
	public class SalesTaxContext : DbContext, ISalesTaxContext
	{

		public DbSet<StateSalesTax> stateSalesTaxes { get; set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="options"></param>
		public SalesTaxContext(DbContextOptions<SalesTaxContext> options) : base(options)
		{
		
		}

		/// <summary>
		/// Add a new state to the database. Accepts a StateSalesTax object.
		/// </summary>
		/// <param name="model">The StateSalesTax model containing state and county information.</param>
		/// <returns>Task </returns>
		public async Task AddState(StateSalesTax model) {
			await stateSalesTaxes.AddAsync(model);
			await SaveChangesAsync();
		}

		/// <summary>
		/// Gets a state from the database given the state name.
		/// </summary>
		/// <param name="name">Name of the state to retrieve. </param>
		/// <returns>StateSalesTax</returns>
		public async Task<StateSalesTax> RetrieveState(string name) {
			return await stateSalesTaxes.Include(state => state.CountyTaxes).FirstOrDefaultAsync(state => state.Name == name);
		}

	}

}
