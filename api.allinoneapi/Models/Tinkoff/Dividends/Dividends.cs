using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.allinoneapi.Models.Tinkoff.Dividends
{
    public class ClosePrice
    {
        public string currency { get; set; }
        public string units { get; set; }
        public int nano { get; set; }
    }

    public class Dividend
    {
        public DividendNet dividendNet { get; set; }
        public DateTime paymentDate { get; set; }
        public DateTime declaredDate { get; set; }
        public DateTime lastBuyDate { get; set; }
        public string dividendType { get; set; }
        public DateTime recordDate { get; set; }
        public string regularity { get; set; }
        public ClosePrice closePrice { get; set; }
        public YieldValue yieldValue { get; set; }
        public DateTime createdAt { get; set; }
    }

    public class DividendNet
    {
        public string currency { get; set; }
        public string units { get; set; }
        public int nano { get; set; }
    }

    public class Dividends
    {
        public List<Dividend> dividends { get; set; }
    }

    public class YieldValue
    {
        public string units { get; set; }
        public int nano { get; set; }
    }
}
