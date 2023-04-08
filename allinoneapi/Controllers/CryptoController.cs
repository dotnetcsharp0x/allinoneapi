using Microsoft.AspNetCore.Mvc;
using allinoneapi.Models;
using Binance.Net.Clients;
using System.Collections;
using BenchmarkDotNet.Attributes;
using allinoneapi.Data;
using RestSharp;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace allinoneapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase, IDisposable
    {
        public CryptoController() { }

        #region UpdatePairs
        [HttpGet]
        [Route("UpdatePairs")]
        public HashSet<Crypto_Symbols> UpdatePairs()
        {
            BinanceClient client = new();
            Console.WriteLine("log");
            var resp = client.SpotApi.ExchangeData.GetProductsAsync().Result.Data.Select(x => new Crypto_Symbols { Symbol = x.Symbol, QuoteAsset = x.QuoteAsset, BaseAsset = x.BaseAsset }).ToHashSet();
            client.Dispose();
            return resp;
        }
        #endregion

        #region UpdateCurrentPrice
        [HttpGet]
        [Route("UpdateCurrentPrice")]
        public HashSet<Crypto_Price> UpdateCurrentPrice()
        {
            BinanceClient client = new();
            var resp = client.SpotApi.ExchangeData.GetPricesAsync().Result.Data.Select(x => new Crypto_Price { Symbol = x.Symbol, Price = x.Price, DateTime = DateTime.Now }).ToHashSet();
            Console.WriteLine(resp);
            client.Dispose();
            return resp;
        }
        [HttpGet]
        [Route("UpdateCurrentPrice2")]
        public HashSet<Binance_Price_quoted> UpdateCurrentPrice2()
        {
            HashSet<Binance_Price_quoted>? serilizerForBinanceRequest;
            string url = "https://api.binance.com/api/v1/ticker/price";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var r = client.Execute(request).Content;
            serilizerForBinanceRequest = JsonSerializer.Deserialize<HashSet<Binance_Price_quoted>>(r);
            if (serilizerForBinanceRequest == null)
            {
                serilizerForBinanceRequest = new HashSet<Binance_Price_quoted>();
            }
            return serilizerForBinanceRequest;
        }
        #endregion

        #region GetKandles
        [HttpGet]
        [Route("GetKandles")]
        public int GetKandles()
        {
            BinanceClient client = new();
            var r = client.SpotApi.ExchangeData.GetKlinesAsync("BTCUSDT",Binance.Net.Enums.KlineInterval.OneMinute, DateTime.Now.AddMinutes(-2), DateTime.Now,1).Result;
            Console.WriteLine(r);
            //var r = client.SpotApi.ExchangeData.GetProductsAsync().Result;
            return 1;
            //await using (allinoneapiContext _context = new allinoneapiContext())
            //{
            //    await _context.Database.ExecuteSqlRawAsync("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
            //    var CurrentPairsInDb = (from c in _context.Crypto_Symbols where c.Symbol=="BTCUSDT" select c);
            //    var BnKandles = new List<Binance_KandlesArray>();
            //    string url = null;
            //    foreach (var i in CurrentPairsInDb) {
            //        url = $"https://api.binance.com/api/v1/klines?symbol={i.Symbol}&interval=1m&limit=1";
            //        var client = new RestClient(url);
            //        var request = new RestRequest(url, Method.Get);
            //        request.AddHeader("Content-Type", "application/json");
            //        var r = client.Execute(request).Content.Replace("[[", "[").Replace("]]", "]");
            //        var result = JsonSerializer.Deserialize<List<Binance_Kanldes>>(r);
            //        //var BnKandlest = JsonSerializer.Deserialize<List<Binance_KandlesArray>>(r);
            //        //Console.WriteLine(BnKandles);
            //    }
            //    return 1;
            //}
        }
        #endregion

        ~CryptoController()
        {
            Console.WriteLine("controller ctor");
        }
        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.SuppressFinalize(this);
            Console.WriteLine("controller dispose");
        }
    }
}