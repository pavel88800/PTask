using System.Collections.Generic;
using System.Threading.Tasks;
using ExchangeRatesWebApp.BL.Interfaces;
using ExchangeRatesWebApp.Controllers.Base;
using ExchangeRatesWebApp.DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRatesWebApp.Controllers.Api
{
    public class ExchangeRatesController : BaseApiController
    {
        private readonly IExchangeRate _exchangeRate;

        public ExchangeRatesController(IExchangeRate exchangeRate)
        {
            _exchangeRate = exchangeRate;
        }
        

        [HttpGet]
        public async Task<IEnumerable<ExchangeRate>> GetExchangeRates()
        {
            var result = await _exchangeRate.GetExchangeRate();
            return result;
        }
    }
}
