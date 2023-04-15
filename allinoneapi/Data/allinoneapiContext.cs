#nullable disable
using Microsoft.EntityFrameworkCore;
using api.allinoneapi.Models;
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

        #region Tables
        public DbSet<Crypto_Symbols> Crypto_Symbols { get; set; }
        public DbSet<Crypto_Price> Crypto_Price { get; set; }

        #endregion

        protected override void ConfigureConventions(
    ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>()
                .HavePrecision(20, 10);
        }
    }
}
