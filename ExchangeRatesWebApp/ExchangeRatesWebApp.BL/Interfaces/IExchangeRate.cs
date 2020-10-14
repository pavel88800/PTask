using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExchangeRatesWebApp.DB.Models;

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
        ///     <see cref="IEnumerable<ExchangeRate>"/>
        /// </returns>
        Task<IEnumerable<ExchangeRate>> GetExchangeRate(DateTime date, string currencyName);
    }
}