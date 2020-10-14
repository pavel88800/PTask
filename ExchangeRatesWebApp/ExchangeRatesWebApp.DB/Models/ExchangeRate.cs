using System;

namespace ExchangeRatesWebApp.DB.Models
{
    public class ExchangeRate
    {
        /// <summary>
        ///     Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Наименование валюты
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        ///     Значение валюты
        /// </summary>
        public decimal CurrencyValue { get; set; }

        /// <summary>
        ///     кол-во валюты
        /// </summary>
        public int CurrencyCount { get; set; }

        /// <summary>
        ///     Дата
        /// </summary>
        public DateTime Date { get; set; }
    }
}