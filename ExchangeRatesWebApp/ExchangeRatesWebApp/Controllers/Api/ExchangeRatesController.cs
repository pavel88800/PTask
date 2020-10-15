using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExchangeRatesWebApp.BL.DTO;
using ExchangeRatesWebApp.BL.Interfaces;
using ExchangeRatesWebApp.Controllers.Base;
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
        public async Task<IEnumerable<ExchangeRateDto>> GetExchangeRates(DateTime date, string currency)
        {
            var result = await _exchangeRate.GetExchangeRate(date, currency);
            return result;
        }

        [HttpGet]
        [Route("/currency")]
        public async Task<IEnumerable<string>> GetCurrency()
        {
            var result = await _exchangeRate.GetCurrency();
            return result;
        }

        [HttpGet]
        [Route("/list-in-year")]
        public async Task<IEnumerable<ExchangeRateDto>> GetExchangeRateFromYear(int date)
        {
            var result = await _exchangeRate.GetExchangeRateFromYear(date);
            return result;
        }
    }
}