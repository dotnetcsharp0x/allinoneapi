using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.allinoneapi.Models
{
    public class Ticker
    {
        public string ticker { get; set; }
        public double todaysChangePerc { get; set; }
        public double todaysChange { get; set; }
        public long updated { get; set; }
        public Day day { get; set; }
        public Min min { get; set; }
        public PrevDay prevDay { get; set; }
    }
}
