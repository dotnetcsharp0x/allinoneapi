namespace allinoneapi.Models
{
    public class Binance_Price_quoted : IDisposable
    {
        public string symbol { get; set; }
        public string price { get; set; }
        #region IDisposable

       ~Binance_Price_quoted()
        {
            //Console.WriteLine($"{symbol} distructor");
        }

        public void Dispose()
        {
            try
            {

            }
            finally
            {
                //Console.WriteLine($"{symbol} has been disposed");
            }
        }

        #endregion IDisposable
    }
}
