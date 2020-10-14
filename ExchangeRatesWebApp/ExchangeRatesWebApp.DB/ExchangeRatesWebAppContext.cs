using System;
using ExchangeRatesWebApp.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRatesWebApp.DB
{
    public class ExchangeRatesWebAppContext : DbContext
    {
        public ExchangeRatesWebAppContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }
    }
}
