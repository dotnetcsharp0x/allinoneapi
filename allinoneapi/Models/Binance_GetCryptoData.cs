using System;

namespace allinoneapi.Models
{
    public class Binance_GetCryptoData : IDisposable
    {
        public string timezone { get; set; }
        public long serverTime { get; set; }
        public Binance_rateLimits[] rateLimits { get; set; }
        public Binance_symbols[] symbols { get; set; }
        #region IDisposable

        ~Binance_GetCryptoData()
        {
            Console.WriteLine($"binance_getcryptodata distructor");
        }

        public void Dispose()
        {
            try { }
            finally
            {
                Console.WriteLine($"binance_getcryptodata has been disposed");
            }
        }

        #endregion IDisposable
    }
}
