using System;
using System.Collections.Generic;
using System.Linq;
using ExchangeRatesConsoleApp.DB;
using ExchangeRatesConsoleApp.DB.Models;

namespace ExchangeRatesConsoleApp.Workers
{
    internal class SaveDataInDbWorker
    {
        /// <summary>
        ///     Сохранить данные в БД.
        /// </summary>
        /// <param name="listExchangeRate">Коллекция курса валют</param>
        public static void Save(List<ExchangeRate> listExchangeRate)
        {
            Console.WriteLine("Производится запись в БД");
            using (var db = new ExchangeRatesConsoleContext())
            {
                var dbResult = db.ExchangeRates.ToList();

                foreach (var res in dbResult)
                    listExchangeRate.RemoveAll(x => x.Date == res.Date && x.CurrencyName == res.CurrencyName);

                db.AddRange(listExchangeRate);
                db.SaveChanges();
            }
        }
    }
}