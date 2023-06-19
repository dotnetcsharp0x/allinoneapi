using CryptoExchange.Net.Interfaces;
using Nancy.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.allinoneapi.Models.Stocks.Polygon.Actions
{
    public class Polygon
    {
        private string api = "1IDknqV7XjsFhZRNtwdNcJOtPp9IH0Ji";
        public Root GetPrice(string symbol = "SPY")
        {
            string url_get = "https://api.polygon.io/v2/aggs/ticker/SPY/range/1/day/2023-06-01/2023-06-02?adjusted=true&sort=asc&limit=120&apiKey=";
            var url = url_get + api;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var r = client.ExecuteAsync(request).Result.Content;

            var Content = new StringContent(r.ToString(), Encoding.UTF8, "application/json");
            JavaScriptSerializer? js = new JavaScriptSerializer();
            var ticker_resp = js.Deserialize<Root>(r);
            return ticker_resp;
        }
    }
}
