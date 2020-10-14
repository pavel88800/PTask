using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangeRatesWebApp.BL.Interfaces;
using ExchangeRatesWebApp.DB;
using ExchangeRatesWebApp.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRatesWebApp.BL.Services
{
    public class ExchangeRateService : IExchangeRate
    {
        private readonly ExchangeRatesWebAppContext _context;

        public ExchangeRateService(ExchangeRatesWebAppContext context)
        {
            _context = context;
        }

        /// <inheritdoc cref="IExchangeRate"/>
        public async Task<IEnumerable<ExchangeRate>> GetExchangeRate(DateTime date, string currencyName)
        {
            try
            {
                var res = _context.ExchangeRates;

                var exchangeRates = currencyName == null
                    ? res.Where(x => x.Date == date)
                    : res.Where(x => x.Date == date && x.CurrencyName == currencyName);

                var result = await exchangeRates.ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                return new List<ExchangeRate>();
            }
        }

        /// <inheritdoc cref="IExchangeRate"/>
        public async Task<IEnumerable<string>> GetCurrency()
        {
            try
            {
                var result = await _context.ExchangeRates.Select(x => x.CurrencyName).Distinct().OrderBy(x=>x).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                return new List<string>();
            }
        }

        /// <inheritdoc cref="IExchangeRate"/>
        public async Task<IEnumerable<ExchangeRate>> GetExchangeRateFromYear(int date)
        {
            try
            {
                var result =  await _context.ExchangeRates.Where(x => x.Date.Year == date).Take(500).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                return new List<ExchangeRate>();
            }
        }
    }
}