using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.allinoneapi.Models.Stocks.Polygon.News
{
    public class InstrumentsNews
    {
        public int Id { get; set; }
        public string? publishername { get; set; }
        public string? publisherhomepage_url { get; set; }
        public string? publisherlogo_url { get; set; }
        public string? publisherfavicon_url { get; set; }
        public string? title { get; set; }
        public string? author { get; set; }
        public DateTime? published_utc { get; set; }
        public string? article_url { get; set; }
        public string? image_url { get; set; }
        public string? description { get; set; }
        public string? amp_url { get; set; }
        public DateTime? update_date { get; set; }
        public List<TickerToNews> tickers { get; set; }
    }
}
