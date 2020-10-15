using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangeRatesWebApp.BL.DTO;
using ExchangeRatesWebApp.BL.Interfaces;
using ExchangeRatesWebApp.DB;
using ExchangeRatesWebApp.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRatesWebApp.BL.Services
{
    public class ExchangeRateService : IExchangeRate
    {
        /// <summary>
        ///     Контекст БД.
        /// </summary>
        private readonly ExchangeRatesWebAppContext _context;

        /// <summary>
        ///     Конструктор.
        /// </summary>
        /// <param name="context">Контекст БД.</param>
        public ExchangeRateService(ExchangeRatesWebAppContext context)
        {
            _context = context;
        }

        /// <inheritdoc cref="IExchangeRate" />
        public async Task<IEnumerable<ExchangeRateDto>> GetExchangeRate(DateTime date, string currencyName)
        {
            try
            {
                var res = _context.ExchangeRates;

                var exchangeRates = currencyName == null
                    ? res.Where(x => x.Date == date).OrderBy(x => x.CurrencyName)
                    : res.Where(x => x.Date == date && x.CurrencyName == currencyName).OrderBy(x => x.CurrencyName);

                var result = await GetEditedData(await exchangeRates.ToListAsync());

                return result;
            }
            catch (Exception e)
            {
                return new List<ExchangeRateDto>();
            }
        }

        /// <inheritdoc cref="IExchangeRate" />
        public async Task<IEnumerable<string>> GetCurrency()
        {
            try
            {
                var result = await _context.ExchangeRates.Select(x => x.CurrencyName).Distinct().OrderBy(x => x)
                    .ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                return new List<string>();
            }
        }

        /// <inheritdoc cref="IExchangeRate" />
        public async Task<IEnumerable<ExchangeRateDto>> GetExchangeRateFromYear(int date)
        {
            try
            {
                var result = await _context.ExchangeRates.Where(x => x.Date.Year == date).Take(500).OrderBy(x => x.Date)
                    .ToListAsync();
                var newResult = await GetEditedData(result);
                return newResult;
            }
            catch (Exception e)
            {
                return new List<ExchangeRateDto>();
            }
        }

        /// <summary>
        ///     Получить отредактированные данные.
        /// </summary>
        /// <param name="list">Колекция Валют из БД.</param>
        /// <returns></returns>
        private async Task<IEnumerable<ExchangeRateDto>> GetEditedData(List<ExchangeRate> list)
        {
            var result = new List<ExchangeRateDto>();
            await Task.Run(() =>
            {
                result = list.Select(x => new ExchangeRateDto
                {
                    CurrencyCount = x.CurrencyCount,
                    CurrencyName = x.CurrencyName,
                    CurrencyValue = x.CurrencyValue,
                    Date = x.Date.ToString("d")
                }).ToList();
            });
            return result;
        }
    }
}