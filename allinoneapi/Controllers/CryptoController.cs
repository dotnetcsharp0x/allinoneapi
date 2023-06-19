using Microsoft.AspNetCore.Mvc;
using api.allinoneapi;
using api.allinoneapi.Models;
using System.Collections.Generic;
using AspNetCoreRateLimit;
using RestSharp;
using Nancy.Json;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace allinoneapi.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase, IDisposable
    {
        Crypto crypto = new Crypto();
        private readonly ILogger<CryptoController> _logger;
        private readonly IIpPolicyStore _policyStore;
        public CryptoController(ILogger<CryptoController> logger,IIpPolicyStore policyStore) {
            _logger = logger;
            _policyStore = policyStore;
        }

        #region GetInstruments
        [HttpGet]
        [Route("GetInstruments")]
        public HashSet<Crypto_Symbols> GetInstruments(int page = 1)
        {

            Binance_GetCryptoData? cryptoPairs;
            Binance_symbols[]? respFromBinance;
            Crypto_Symbols? serializerCryptoData = new Crypto_Symbols();
            List<Crypto_Symbols>? cryptoSymbolListOnResponse = new List<Crypto_Symbols>();
            string url1 = "https://api.binance.com/api/v1/exchangeInfo";
            var client1 = new RestClient(url1);
            var request1 = new RestRequest(url1, Method.Get);
            request1.AddHeader("Content-Type", "application/json");
            var r1 = client1.ExecuteAsync(request1).Result.Content;
            JavaScriptSerializer? js1 = new JavaScriptSerializer();
            cryptoPairs = js1.Deserialize<Binance_GetCryptoData>(r1);
            respFromBinance = cryptoPairs.symbols;
            var binance_resp = (from i in respFromBinance
                                select new Crypto_Symbols()
                                {
                                    Symbol = i.symbol,
                                    BaseAsset = i.baseAsset,
                                    QuoteAsset= i.quoteAsset
                                }).ToHashSet();
            Console.WriteLine(binance_resp);

            List<CoinGeckoDataCoins?> gecko_symbols = new List<CoinGeckoDataCoins?>();
            var url = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=BTC&order=market_cap_desc&per_page=250&page="+page+"&sparkline=false&locale=en";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var r = client.ExecuteAsync(request).Result.Content;
            if (r is not null)
            {
                var Content = new StringContent(r.ToString(), Encoding.UTF8, "application/json");
                JavaScriptSerializer? js = new JavaScriptSerializer();
                gecko_symbols = js.Deserialize<List<CoinGeckoDataCoins?>>(r);

                //var binance_resp = crypto.Binance_GetSymbols();
                foreach(var a in binance_resp)
                {
                    a.source = "Binance";
                }
                //var binance_resp = new HashSet<Crypto_Symbols>();
                var resp_gecko_symbols_format =(
                    from p in gecko_symbols
                    select new Crypto_Symbols { 
                        Symbol = p.symbol.ToUpper(), 
                        BaseAsset=null,
                        QuoteAsset=null, 
                        circulating_supply=p.circulating_supply,
                        total_supply=p.total_supply,
                        max_supply=p.max_supply,
                        domination=null
                    }).ToHashSet();
                foreach(var a in resp_gecko_symbols_format)
                {
                    binance_resp.Add(a);
                }
                return binance_resp;
            }
            else
            {
                return new HashSet<Crypto_Symbols> { };
            }
        }
        #endregion

        #region UpdateCurrentPrice
        [HttpGet]
        [Route("GetPrices")]
        public HashSet<Crypto_Price> GetPrices()
        {
            return crypto.Binance_GetCurrentPrices();
        }
        #endregion

        #region DayOfDayData
        [HttpGet]
        [Route("DayOfDayData")]

        public IEnumerable<Binance_CryptoKandles> DayOfDayData(string? symbol="BTCUSDT")
        {
            return crypto.Binance_DayOfDayData(symbol);
        }
        #endregion

        #region GetKandles
        [HttpGet]
        [Route("GetKandles")]
        public HashSet<Binance_CryptoKandles> GetKandles(string? symbol = "BTCUSDT",int minutes=1,int lines=1,string interval="5m")
        {
            try
            {
                int to_minus = -182;
                to_minus -= minutes;
                var resp = crypto.Binance_GetKandles(symbol, to_minus, lines, interval);
                if (resp != null)
                {
                    return resp;
                }
                else
                {
                    return new HashSet<Binance_CryptoKandles>() { };
                }
            }
            catch (Exception ex)
            {
                return new HashSet<Binance_CryptoKandles>() { };
            }
        }
        #endregion

        #region DisposeCtor
        ~CryptoController()
        {
            Console.WriteLine("controller ctor");
        }
        public void Dispose()
        {
            crypto.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.SuppressFinalize(this);
            Console.WriteLine("controller dispose");
        }

        #endregion
    }
}