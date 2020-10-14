using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<IEnumerable<ExchangeRate>> GetExchangeRate()
        {
            var res = await _context.ExchangeRates.ToListAsync();
            return res;
        }
    }
}
