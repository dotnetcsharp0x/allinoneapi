using System.ComponentModel.DataAnnotations;

namespace allinoneapi.Models
{
    public class Crypto_Price
    {
        public int Id { get; set; }
        public string? Symbol { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }
    }
}
