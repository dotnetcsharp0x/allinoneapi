using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.allinoneapi.Models.Stocks.Polygon.News
{
    public class TickerToNews
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? ticker { get; set; }
        public int InstrumentsNewsId { get; set; }
        public InstrumentsNews InstrumentsNews { get; set; }
    }
}
