﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.allinoneapi.Models.Stocks.Polygon
{
    public class Results
    {
        public string ticker { get; set; }
        public string name { get; set; }
        public string market { get; set; }
        public string locale { get; set; }
        public string primary_exchange { get; set; }
        public string type { get; set; }
        public bool active { get; set; }
        public string currency_name { get; set; }
        public string cik { get; set; }
        public string composite_figi { get; set; }
        public string share_class_figi { get; set; }
        public double market_cap { get; set; }
        public string phone_number { get; set; }
        public Address address { get; set; }
        public string description { get; set; }
        public string sic_code { get; set; }
        public string sic_description { get; set; }
        public string ticker_root { get; set; }
        public string homepage_url { get; set; }
        public int total_employees { get; set; }
        public string list_date { get; set; }
        public Branding branding { get; set; }
        public double share_class_shares_outstanding { get; set; }
        public double weighted_shares_outstanding { get; set; }
        public int round_lot { get; set; }
    }
}
