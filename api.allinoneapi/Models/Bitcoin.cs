using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.allinoneapi.Models
{
    public class Bitcoin
    {
        public List<AddressStat> addressStats { get; set; }
        public List<Block> blocks { get; set; }
    }
}
