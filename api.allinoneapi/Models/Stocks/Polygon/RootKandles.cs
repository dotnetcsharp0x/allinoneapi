using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.allinoneapi.Models.Stocks.Polygon
{
    public class RootKandles
    {
        public List<Ticker> tickers { get; set; }
        public string status { get; set; }
        public string request_id { get; set; }
        public int count { get; set; }
    }
}
