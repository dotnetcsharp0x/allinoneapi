using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.allinoneapi.Models.Stocks.Polygon
{
    public class TickerDescription
    {
        public string request_id { get; set; }
        public Results results { get; set; }
        public string status { get; set; }
    }
}
