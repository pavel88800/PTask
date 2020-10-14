using ExchangeRatesWebApp.BL.Interfaces;
using ExchangeRatesWebApp.BL.Services;
using ExchangeRatesWebApp.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ExchangeRatesWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddControllers();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "ASP.NET Core Web API"
                });
            }).AddDbContext<ExchangeRatesWebAppContext>(options => options.UseSqlServer(connection));

            ImplementDependency(services);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1"); });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        /// <summary>
        ///     Внедряем зависимости.
        /// </summary>
        /// <param name="services"></param>
        private void ImplementDependency(IServiceCollection services)
        {
            services
                .AddScoped<IExchangeRate, ExchangeRateService>();
        }
    }
}