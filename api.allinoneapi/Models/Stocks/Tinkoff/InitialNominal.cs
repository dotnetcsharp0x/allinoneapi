using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.allinoneapi.Models.Stocks.Tinkoff
{
    public class InitialNominal
    {
        public string currency { get; set; }
        public int units { get; set; }
        public int nano { get; set; }
    }
}
