namespace allinoneapi.Models
{
    public class Binance_Price : IDisposable
    {
        public string symbol { get; set; }
        public decimal price { get; set; }
        #region IDisposable

       ~Binance_Price()
        {
            Console.WriteLine($"{symbol} distructor");
        }

        public void Dispose()
        {
            try { } finally {
                Console.WriteLine($"{symbol} has been disposed");
            }
        }

        #endregion IDisposable
    }
}
