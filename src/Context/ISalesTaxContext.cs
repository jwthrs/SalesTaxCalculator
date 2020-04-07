using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesTaxCalculator.Models;

namespace SalesTaxCalculator.Context
{
    public interface ISalesTaxContext
    {
        Task AddState(StateSalesTax state);

        Task<StateSalesTax> RetrieveState(string name);
    }
}
