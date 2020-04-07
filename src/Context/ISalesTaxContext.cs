using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesTaxCalculator.Models;

namespace SalesTaxCalculator.Context
{
    /// <summary>
    /// Interface for interacting with the database through Entity Framework.
    /// Contains required methods that defines a SalesTaxContent.
    /// </summary>
    public interface ISalesTaxContext
    {
        /// <summary>
        /// Add a new state to the database. Accepts a StateSalesTax object.
        /// </summary>
        /// <param name="model">The StateSalesTax model containing state and county information.</param>
        /// <returns>Task </returns>
        Task AddState(StateSalesTax state);

        /// <summary>
        /// Gets a state from the database given the state name.
        /// </summary>
        /// <param name="name">Name of the state to retrieve. </param>
        /// <returns>StateSalesTax</returns>
        Task<StateSalesTax> RetrieveState(string name);
    }
}
