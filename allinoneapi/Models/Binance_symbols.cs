namespace allinoneapi.Models
{
    public class Binance_symbols : IDisposable
    {
        public int Id { get; set; }
        public string symbol { get; set; }
        public string status { get; set; }
        public string baseAsset { get; set; }
        public int baseAssetPrecision { get; set; }
        public string quoteAsset { get; set; }
        public int quotePrecision { get; set; }
        public int quoteAssetPrecision { get; set; }
        public int baseCommissionPrecision { get; set; }
        public int quoteCommissionPrecision { get; set; }
        #region IDisposable

        ~Binance_symbols()
        {
            //Console.WriteLine($"{symbol} distructor");
        }

        public void Dispose()
        {
            try { }
            finally
            {
                //Console.WriteLine($"{symbol} has been disposed");
            }
        }

        #endregion IDisposable
    }
}
