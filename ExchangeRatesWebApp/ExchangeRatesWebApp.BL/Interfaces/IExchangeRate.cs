using System.Collections.Generic;
using System.Threading.Tasks;
using ExchangeRatesWebApp.DB.Models;

namespace ExchangeRatesWebApp.BL.Interfaces
{
    public interface IExchangeRate
    {
        Task<IEnumerable<ExchangeRate>> GetExchangeRate();
    }
}