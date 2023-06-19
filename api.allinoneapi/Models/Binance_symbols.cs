namespace api.allinoneapi.Models
{
    public class Binance_symbols
    {
        public int Id { get; set; }
        public string? symbol { get; set; }
        public string? status { get; set; }
        public string? baseAsset { get; set; }
        public int baseAssetPrecision { get; set; }
        public string? quoteAsset { get; set; }
        public int quotePrecision { get; set; }
        public int quoteAssetPrecision { get; set; }
        public int baseCommissionPrecision { get; set; }
        public int quoteCommissionPrecision { get; set; }
        public double? circulating_supply { get; set; }
        public double? total_supply { get; set; }
        public double? max_supply { get; set; }
        public double? domination { get; set; }
    }
}
