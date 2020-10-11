using ExchangeRatesConsoleApp.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRatesConsoleApp.DB
{
    public class ExchangeRatesConsoleContext : DbContext
    {
        public ExchangeRatesConsoleContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=PlariumDB;Trusted_Connection=True;");
        }
    }
}