namespace allinoneapi.Models
{
    public class Binance_CyptoPrice : IDisposable
    {
        public int Id { get; set; }
        public string? Symbol { get; set; }
        public float Price { get; set; }
        #region IDisposable

        ~Binance_CyptoPrice()
        {
            Console.WriteLine($"{Symbol} distructor");
        }

        public void Dispose()
        {
            try
            {

            }
            finally { Console.WriteLine($"{Symbol} has been disposed"); }
            
        }

        #endregion IDisposable
    }
}
