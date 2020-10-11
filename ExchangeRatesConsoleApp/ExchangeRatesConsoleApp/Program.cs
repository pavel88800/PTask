using System;
using System.Threading;
using ExchangeRatesConsoleApp.Workers;

namespace ExchangeRatesConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var currencyWorker = new CurrencyWorker();

            var year = GetYear();
            var list = currencyWorker.Main(year);
        }

        private static int GetYear()
        {
            var year = new DateTime().Year;
            try
            {
                Console.WriteLine("Введите год, за который нужно получить курс валют");
                year = int.Parse(Console.ReadLine());
                Console.WriteLine("-------------------------------------");
                return year;
            }
            catch 
            {
                Console.WriteLine("Введено неверное значения для года. Выборку будет производиться по текущему году.");
                Thread.Sleep(2000);
                return year;
            }
        }
    }
}