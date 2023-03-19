using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System;
using RestSharp;
using System.Text;
using System.Net.Http;
using allinoneapi.Data;
using allinoneapi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;

namespace allinoneapi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentsController : ControllerBase, IDisposable
    {
        allinoneapiContext _context;
        List<Binance_Price> _bp = new List<Binance_Price>();
        List<Binance_Price_quoted> _BinancePrices;
        List<Crypto_Price> _CryptoPricesList = new List<Crypto_Price>();
        List<Crypto_Symbols> _cs = new List<Crypto_Symbols>();
        Binance_symbols[] _resp;
        HttpClient _httpClient = new HttpClient();
        Crypto_Price _CryptoPrices;
        Binance_GetCryptoData _CryptoPairs;
        Crypto_Symbols _cs_detailed;
        public InstrumentsController(allinoneapiContext httpcontext)
            {
            _context = httpcontext;
        }
        //#region Test
        //[HttpGet]
        //[Route("GetPrice")]
        ////[Route("[GetPrice]")]
        //public getPrice GetPrice()
        //{
        //    string url = "https://api.binance.com/api/v1/ticker/price?symbol=BTCUSDT";
        //    var client = new RestClient(url);
        //    var request = new RestRequest(url, Method.Get);
        //    request.AddHeader("Content-Type", "application/json");
        //    var r = client.ExecuteAsync(request).Result.Content;
        //    getPrice cr;
        //    if (r is not null)
        //    {
        //        var Content = new StringContent(r.ToString(), Encoding.UTF8, "application/json");
        //        cr = cr = JsonSerializer.Deserialize<getPrice>(r);
        //        //using var response = await httpClient.GetAsync("https://api.binance.com/api/v1/ticker/price?symbol=BTCUSDT");
        //        //cr = await response.Content.ReadFromJsonAsync<Price>();
        //    }
        //    else
        //    {
        //        cr = new getPrice();
        //    }
        //    return cr;
        //}
        //[HttpGet]
        //[Route("GetPriceNew")]
        ////[Route("[GetPrice]")]
        //public Binance_CyptoPrice GetPriceNew()
        //{
        //    Binance_CyptoPrice cr;
        //    using (allinoneapiContext _context = new allinoneapiContext())
        //    {
        //        string url = "https://api.binance.com/api/v1/ticker/price?symbol=BTCUSDT";
        //        var client = new RestClient(url);
        //        var request = new RestRequest(url, Method.Get);
        //        request.AddHeader("Content-Type", "application/json");
        //        var r = client.ExecuteAsync(request).Result.Content;

        //        if (r is not null)
        //        {
        //            var Content = new StringContent(r.ToString(), Encoding.UTF8, "application/json");
        //            cr = JsonSerializer.Deserialize<Binance_CyptoPrice>(r);
        //            //using var response = await httpClient.GetAsync("https://api.binance.com/api/v1/ticker/price?symbol=BTCUSDT");
        //            //cr = await response.Content.ReadFromJsonAsync<Price>();
        //        }
        //        else
        //        {
        //            cr = new Binance_CyptoPrice();
        //        }
        //        _context.Add(cr);
        //        _context.SaveChanges();
        //    }
        //    return cr;
        //}
        //#endregion
        #region UpdatePairs
        [HttpGet]
        [Route("UpdatePairs")]
        public async Task<List<Crypto_Symbols>> UpdatePairs()
        {
            string url = "https://api.binance.com/api/v1/exchangeInfo";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var r = client.ExecuteAsync(request).Result.Content;
            //CryptoPairs = js.Deserialize<Binance_GetCryptoData>(r);
            _CryptoPairs = JsonSerializer.Deserialize<Binance_GetCryptoData>(r);
            _resp = _CryptoPairs.symbols;
            await _context.Database.ExecuteSqlRawAsync("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
            var CurrentPairsInDatabase = await (from i in _context.Crypto_Symbols select i).ToArrayAsync();
            if (r is not null)
            {
                _resp = (from s in _resp where !(from b in CurrentPairsInDatabase select b.Symbol).Contains(s.symbol) select s).ToArray();
                if (_resp.Length > 0)
                {
                    foreach (var i in _resp)
                    {
                        _cs_detailed = new Crypto_Symbols();
                        _cs_detailed.Symbol = i.symbol;
                        _cs_detailed.BaseAsset = i.baseAsset;
                        _cs_detailed.QuoteAsset = i.quoteAsset;
                        _cs.Add(_cs_detailed);
                        await _context.AddAsync(_cs_detailed);
                        _cs_detailed.Dispose();
                    }
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                _resp=null;
                _cs.Clear();
                _CryptoPairs.Dispose();
            }
            return _cs;
        }
#endregion
        #region UpdateCurrentPrice
        [HttpGet]
        [Route("UpdateCurrentPrice")]
        public async Task<List<Crypto_Price>> UpdateCurrentPrice()
        {
            _CryptoPricesList.Clear();
            string url = "https://api.binance.com/api/v1/ticker/price";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var r = client.Execute(request).Content;
            _BinancePrices = JsonSerializer.Deserialize<List<Binance_Price_quoted>>(r);

            //await using (allinoneapiContext _context = new allinoneapiContext())
            //{
                await _context.Database.ExecuteSqlRawAsync("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
                var CurrentPairsInDatabase = await (from i in _context.Crypto_Price select i).ToArrayAsync();
                foreach (var a in _BinancePrices)
                {
                    _CryptoPrices = (from d in CurrentPairsInDatabase where d.Symbol == a.symbol select d).FirstOrDefault();
                    if (_CryptoPrices is null)
                    {
                    _CryptoPrices = new Crypto_Price();
                    _CryptoPrices.Symbol = a.symbol;
                    _CryptoPrices.Price = Convert.ToDecimal(a.price.Replace(".", ","));
                    _CryptoPrices.DateTime = DateTime.Now;
                    _CryptoPricesList.Add(_CryptoPrices);
                    await _context.AddAsync(_CryptoPrices);
                        
                    }
                    else
                    {
                    _CryptoPrices.Symbol = a.symbol;
                    _CryptoPrices.Price = Convert.ToDecimal(a.price.Replace(".",","));
                    _CryptoPrices.DateTime = DateTime.Now;
                    _CryptoPricesList.Add(_CryptoPrices);
                    }
                }
                await _context.SaveChangesAsync();
            return _CryptoPricesList;
        }
        ~InstrumentsController()
        {
            Console.WriteLine("controller distructor");
        }
        public void Dispose()
        {
            try
            {
                
            }
            finally
            {
                GC.Collect(2);
                GC.WaitForPendingFinalizers();
                GC.SuppressFinalize(this);
                Console.WriteLine("controller dispose");

            }
        }
        #endregion
    }
}