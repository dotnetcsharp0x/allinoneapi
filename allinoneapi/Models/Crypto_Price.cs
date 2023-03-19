using System.ComponentModel.DataAnnotations;

namespace allinoneapi.Models
{
    public class Crypto_Price : IDisposable
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }
        #region IDisposable
        ~Crypto_Price()
        {
            //Console.WriteLine($"{Symbol} distructor");
        }

        public void Dispose()
        {
            try {

            }
            finally
            {
                //Console.WriteLine($"{Symbol} has been disposed");
            }
        }
        #endregion IDisposable
    }
}
