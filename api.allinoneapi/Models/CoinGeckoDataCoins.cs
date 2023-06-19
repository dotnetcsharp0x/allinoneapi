using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api.allinoneapi.Models
{
    public class CoinGeckoDataCoins
    {
        public string id { get; set; }
        public string? symbol { get; set; }
        public string name { get; set; }
        string image { get; set; }
        float current_price { get; set; }
        int market_cap { get; set; }
        int market_cap_rank { get; set; }
        int fully_diluted_valuation { get; set; }
        int total_volume { get; set; }
        float high_24h { get; set; }
        float low_24h { get; set; }
        float price_change_24h { get; set; }
        float price_change_percentage_24h { get; set; }
        float market_cap_change_24h { get; set; }
        float market_cap_change_percentage_24h { get; set; }
        public double? circulating_supply { get; set; }
        public double? total_supply { get; set; }
        public double? max_supply { get; set; }
        float ath { get; set; }
        float ath_change_percentage { get; set; }
        string ath_date { get; set; }
        float atl { get; set; }
        float atl_change_percentage { get; set; }
        string atl_date { get; set; }
        CoinGeckoDataCoinsRoi? Roi {get;set;}
        string last_updated { get; set; }
    }
}
