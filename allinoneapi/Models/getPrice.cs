namespace allinoneapi.Models
{
    public class getPrice : IDisposable
    {
        public string? symbol { get; set; }
        public float price { get; set; }
        #region IDisposable

        ~getPrice()
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
