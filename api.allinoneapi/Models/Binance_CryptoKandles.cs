namespace api.allinoneapi.Models
{
    public class Binance_CryptoKandles
    {
        public int Id { get; set; }
        public string? symbol { get; set; }
        public string? source { get; set; }
        public DateTime openTime { get; set; }
        public decimal openPrice { get; set; }
        public decimal highPrice { get; set; }
        public decimal lowPrice { get; set; }
        public decimal closePrice { get; set; }
        public decimal volume { get; set; }
        public DateTime closeTime { get; set; }
        public decimal quoteVolume { get; set; }
        public decimal tradeCount { get; set; }
        public decimal takerBuyBaseVolume { get; set; }
        public decimal takerBuyQuoteVolume { get; set; }
    }
}
