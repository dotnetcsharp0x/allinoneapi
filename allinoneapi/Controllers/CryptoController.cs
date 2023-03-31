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
    public class CryptoController : ControllerBase, IDisposable
    {
        //Process currentProc = Process.GetCurrentProcess();
        public CryptoController() {
        }
        #region UpdatePairs
        [HttpGet]
        [Route("UpdatePairs")]
        public async Task<List<Crypto_Symbols>> UpdatePairs()
        {
            Binance_GetCryptoData? cryptoPairs;
            Binance_symbols[]? respFromBinance;
            Crypto_Symbols? serializerCryptoData = new Crypto_Symbols();
            List<Crypto_Symbols>? cryptoSymbolListOnResponse = new List<Crypto_Symbols>();
            string url = "https://api.binance.com/api/v1/exchangeInfo";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var r = client.ExecuteAsync(request).Result.Content;
            cryptoPairs = JsonSerializer.Deserialize<Binance_GetCryptoData>(r);
            respFromBinance = cryptoPairs.symbols;

            using (allinoneapiContext _context = new allinoneapiContext())
            {
                await _context.Database.ExecuteSqlRawAsync("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
                var CurrentPairsInDatabase = await (from i in _context.Crypto_Symbols select i).ToArrayAsync();
                if (r is not null)
                {
                    respFromBinance = (from s in respFromBinance where !(from b in CurrentPairsInDatabase select b.Symbol).Contains(s.symbol) select s).ToArray();
                    if (respFromBinance.Length > 0)
                    {
                        foreach (var i in respFromBinance)
                        {
                            serializerCryptoData.Symbol = i.symbol;
                            serializerCryptoData.BaseAsset = i.baseAsset;
                            serializerCryptoData.QuoteAsset = i.quoteAsset;
                            cryptoSymbolListOnResponse.Add(serializerCryptoData);
                            await _context.AddAsync(serializerCryptoData);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return cryptoSymbolListOnResponse;
        }
        #endregion

        #region UpdateCurrentPrice
        [HttpGet]
        [Route("UpdateCurrentPrice")]
        public async Task<List<Crypto_Price>> UpdateCurrentPrice()
        {
            List<Crypto_Price>? cryptoPriceListOnResponse = new List<Crypto_Price>();
            List<Binance_Price_quoted>? serilizerForBinanceRequest;
            Crypto_Price? currentPairsInDb;

            string url = "https://api.binance.com/api/v1/ticker/price";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var r = client.Execute(request).Content;
            serilizerForBinanceRequest = JsonSerializer.Deserialize<List<Binance_Price_quoted>>(r);

            using (allinoneapiContext _context = new allinoneapiContext())
            {
                await _context.Database.ExecuteSqlRawAsync("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
                var CurrentPairsInDatabase = await (from i in _context.Crypto_Price select i).ToArrayAsync();
                foreach (var a in serilizerForBinanceRequest)
                {
                    currentPairsInDb = (from d in CurrentPairsInDatabase where d.Symbol == a.symbol select d).FirstOrDefault();
                    if (currentPairsInDb is null)
                    {
                        currentPairsInDb = new Crypto_Price();
                        currentPairsInDb.Symbol = a.symbol;
                        currentPairsInDb.Price = Convert.ToDecimal(a.price.Replace(".", ","));
                        currentPairsInDb.DateTime = DateTime.Now;
                        cryptoPriceListOnResponse.Add(currentPairsInDb);
                        await _context.AddAsync(currentPairsInDb);
                    }
                    else
                    {
                        currentPairsInDb.Symbol = a.symbol;
                        currentPairsInDb.Price = Convert.ToDecimal(a.price.Replace(".", ","));
                        currentPairsInDb.DateTime = DateTime.Now;
                        cryptoPriceListOnResponse.Add(currentPairsInDb);
                    }
                }
                await _context.SaveChangesAsync();
            }
            //var bytesInUse = currentProc.PrivateMemorySize64;
            return cryptoPriceListOnResponse;
        }
        ~CryptoController()
        {
            Console.WriteLine("controller dtor");
        }
        public void Dispose()
        {
            try
            {
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.SuppressFinalize(this);
                Console.WriteLine("controller dispose");
                //var bytesInUse = currentProc.PrivateMemorySize64;
            }
        }
        #endregion
    }
}