using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ExchangeRatesConsoleApp.DB.Models;

namespace ExchangeRatesConsoleApp.Workers
{
    /// <summary>
    ///     Воркер дл работы с валютами.
    /// </summary>
    internal class CurrencyWorker
    {
        public List<ExchangeRate> Main(int year)
        {
            var client = new WebClient();
            var clientRead =
                client.OpenRead(
                    $"https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/year.txt?year={year}");
            var listCurrency = new List<ExchangeRate>();

            Task.Run(() => ReadExchangeRates(clientRead, ref listCurrency));
            

            return listCurrency;
        }

        /// <summary>
        ///     Прочитать стрим и получить данные в файле
        /// </summary>
        /// <param name="clientRead">Стрим</param>
        /// <param name="listCurrency">Коллекция валют</param>
        private static void ReadExchangeRates(Stream clientRead, ref List<ExchangeRate> listCurrency)
        {
            using (var stream = clientRead)
            {
                using (var reader = new StreamReader(stream))
                {
                    using (var sw = new StreamWriter(Directory.GetCurrentDirectory() + "\\exchange_rate_fixing.txt"))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null) sw.WriteLine(line);
                    }


                    var fileRead = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\exchange_rate_fixing.txt");

                    var cells = (from l in fileRead
                        select l.Split("|".ToArray(), StringSplitOptions.RemoveEmptyEntries)).ToArray();

                    ReadingCells(cells, ref listCurrency);
                }
            }
        }

        /// <summary>
        ///     Прочитать ячейки "таблицы"
        /// </summary>
        /// <param name="cells">Ячейки</param>
        /// <param name="listCurrency">Коллекция валют</param>
        private static void ReadingCells(string[][] cells, ref List<ExchangeRate> listCurrency)
        {
            for (var i = 0; i < cells.Length; i++)
            {
                var nameCurrency = string.Empty;
                var countCurrency = 0;

                if (i < cells[i].Length && i > 0)
                {
                    var arrStr = cells[0][i].Split(" ");
                    nameCurrency = arrStr[1];
                    countCurrency = int.Parse(arrStr[0]);
                }

                WriteInListCurrency(i, cells, countCurrency, nameCurrency, ref listCurrency);
            }

            /*foreach (var a in listCurrency)
                Console.WriteLine(
                    $"Name:{a.CurrencyName}  Count:{a.CurrencyCount}  CurrencyValue:{a.CurrencyValue}  Date:{a.Date}");*/
        }

        /// <summary>
        ///     Запись в коллекцию валют
        /// </summary>
        /// <param name="iteration">Итерация</param>
        /// <param name="cells">Ячейки</param>
        /// <param name="countCurrency">Кол-во валюты</param>
        /// <param name="nameCurrency">Наименование валюты</param>
        /// <param name="listCurrency">Коллекция валют</param>
        private static void WriteInListCurrency(int iteration, string[][] cells, int countCurrency, string nameCurrency,
            ref List<ExchangeRate> listCurrency)
        {
            for (var j = 1; j < cells.Length; j++)
                if (iteration < cells[iteration].Length && iteration > 0)
                {
                    var dateCurrency = DateTime.Parse(cells[j][0]);
                    var currencyValue = decimal.Parse(cells[j][iteration], NumberStyles.AllowCurrencySymbol |
                                                                           NumberStyles.AllowDecimalPoint |
                                                                           NumberStyles.AllowThousands,
                        CultureInfo.InvariantCulture);

                    listCurrency.Add(new ExchangeRate
                    {
                        CurrencyCount = countCurrency,
                        CurrencyName = nameCurrency,
                        Date = dateCurrency,
                        CurrencyValue = currencyValue
                    });
                }
        }
    }
}