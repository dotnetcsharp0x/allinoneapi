using api.allinoneapi.Interfaces;

namespace api.allinoneapi.Models
{
    public class Crypto_Symbols : ICrypto_Symbols
    {
        public int Id { get; set; }
        public string? Symbol { get; set; }
        public string? BaseAsset { get; set; }
        public string? QuoteAsset { get; set; }
        public double? circulating_supply { get; set; }
        public double? total_supply { get; set; }
        public double? max_supply { get; set; }
        public double? domination { get; set; }
        public string? source { get; set; }
    }
}
