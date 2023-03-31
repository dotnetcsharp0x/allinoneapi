#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using allinoneapi.Models;
using Microsoft.Identity.Client;
using System.Text.Json.Serialization;
//using System.Data.Entity;

namespace allinoneapi.Data
{
    public class allinoneapiContext : DbContext, IDisposable
    {
        private string connectionString;
        public allinoneapiContext ()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            connectionString = configuration.GetConnectionString("allinoneapiContext").ToString();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        
        public DbSet<Crypto_Symbols> Crypto_Symbols { get; set; }
        public DbSet<Crypto_Price> Crypto_Price { get; set; }
        protected override void ConfigureConventions(
    ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>()
                .HavePrecision(20, 10);
        }
        public void Dispose()
        {
            try
            {
            }
            finally
            {
                Console.WriteLine("context dispose");
            }
        }
    }
}
