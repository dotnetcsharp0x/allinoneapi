using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.allinoneapi.Models.Stocks.Polygon.Dividends
{
    public class Dividends
    {
        public List<api.allinoneapi.Models.Stocks.Polygon.Dividends.Result> results { get; set; }
        public string status { get; set; }
        public string request_id { get; set; }
        public string next_url { get; set; }
    }

    public class Result
    {
        public int Id { get; set; }
        public double? cash_amount { get; set; }
        public string? currency { get; set; }
        public string? declaration_date { get; set; }
        public string? dividend_type { get; set; }
        public string? ex_dividend_date { get; set; }
        public int? frequency { get; set; }
        public string? pay_date { get; set; }
        public string? record_date { get; set; }
        public string? ticker { get; set; }
        public DateTime? update_date { get; set; }
    }
}
