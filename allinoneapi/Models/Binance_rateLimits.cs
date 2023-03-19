namespace allinoneapi.Models
{
    public class Binance_rateLimits : IDisposable
    {
        public string rateLimitType { get; set; }
        public string interval { get; set; }
        public int intervalNum { get; set; }
        public int limit { get; set; }
        #region IDisposable

        ~Binance_rateLimits()
        {
            Console.WriteLine($"Binance_rateLimits distructor");
        }

        public void Dispose()
        {
            try { }
            finally
            {
                Console.WriteLine($"Binance_rateLimits has been disposed");
            }
        }

        #endregion IDisposable
    }
}
