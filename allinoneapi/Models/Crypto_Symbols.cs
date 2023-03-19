namespace allinoneapi.Models
{
    public class Crypto_Symbols : IDisposable
    {
        public int Id { get; set; }
        public string? Symbol { get; set; }
        public string? BaseAsset { get; set; }
        public string? QuoteAsset { get; set; }

        #region IDisposable

       ~Crypto_Symbols()
        {
            //Console.WriteLine($"{Symbol} distructor");
        }

        public void Dispose()
        {
            try
            {

            }
            finally
            {
                //Console.WriteLine($"{Symbol} has been disposed");
            }
        }

        #endregion IDisposable
    }

}
