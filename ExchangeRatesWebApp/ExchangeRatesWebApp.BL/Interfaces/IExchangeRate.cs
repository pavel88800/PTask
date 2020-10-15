using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExchangeRatesWebApp.BL.DTO;

namespace ExchangeRatesWebApp.BL.Interfaces
{
    public interface IExchangeRate
    {
        /// <summary>
        ///     Выборка из БД курса валют
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="currencyName">Наименование валюты</param>
        /// <returns>
        ///     <see cref="ExchangeRateDto"/>
        /// </returns>
        Task<IEnumerable<ExchangeRateDto>> GetExchangeRate(DateTime date, string currencyName);

        /// <summary>
        ///     Получить валюты
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetCurrency();

        /// <summary>
        ///     Получить записи за год.
        /// </summary>
        /// <param name="date">Год за который выводить</param>
        /// <returns>
        ///     <see cref="ExchangeRateDto"/>
        /// </returns>
        Task<IEnumerable<ExchangeRateDto>> GetExchangeRateFromYear(int date);
    }
}