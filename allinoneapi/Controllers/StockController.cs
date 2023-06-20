using api.allinoneapi;
using api.allinoneapi.Models;
using api.allinoneapi.Models.Stocks.Polygon;
using api.allinoneapi.Models.Stocks.Polygon.Actions;
using api.allinoneapi.Models.Stocks.Polygon.News;
using Google.Protobuf.Collections;
using IBApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Nancy.Json;
using RestSharp;
using System.Net;
using System.Text;
using System.Xml;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace allinoneapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase, IDisposable
    {
        private readonly InvestApiClient _investApi;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly ILogger<Stocks_Data> _logger;
        EClientSocket _clientSocket;
        public readonly EReaderSignal _Signal;
        Stocks_Data _stock;
        private static string api = "&apiKey=";
        private static string api2 = "?apiKey=";
        public StockController(ILogger<Stocks_Data> logger, InvestApiClient investApi, IHostApplicationLifetime lifetime)
        {
            XmlDocument xApi = new XmlDocument();
            xApi.Load("api.xml");
            XmlElement? xRootApi = xApi.DocumentElement;
            if (xRootApi != null)
            {
                foreach (XmlElement xnode in xRootApi)
                {
                    api = api + xnode.ChildNodes[1].InnerText;
                    api2 = api2 + xnode.ChildNodes[1].InnerText;
                }
            }
            _logger = logger;
            _lifetime = lifetime;
            _investApi = investApi;
            _stock = new Stocks_Data(_logger, _investApi, _lifetime);
        }

        #region GetNews
        [HttpGet]
        [Route("GetNews")]
        public HashSet<InstrumentsNews> GetNews(string? ticker="",int? limit=null)
        {
            string url = "https://api.polygon.io/v2/reference/news?limit=" + limit;
            //ticker=AAPL&
            if (ticker.Length > 0)
            {
                url = url + "&ticker=" + ticker;
            }
            url = url + api;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var r = client.ExecuteAsync(request).Result.Content;

            var Content = new StringContent(r.ToString(), Encoding.UTF8, "application/json");
            JavaScriptSerializer? js = new JavaScriptSerializer();
            var poly_tickers = js.Deserialize<PolygonNews>(r);
            var resp = (from i in poly_tickers.results
                        select new InstrumentsNews()
                        {
                            publishername = i.publisher.name
                            ,publisherhomepage_url = i.publisher.homepage_url
                            ,publisherlogo_url = i.publisher.logo_url
                            ,publisherfavicon_url = i.publisher.favicon_url
                            ,title=i.title
                            ,author=i.author
                            ,published_utc=DateTime.Parse(i.published_utc.ToString())
                            ,article_url=i.article_url
                            ,image_url=i.image_url
                            ,description=i.description
                            ,amp_url=i.amp_url
                            ,update_date=DateTime.Now
                            ,tickers = (from h in i.tickers select new TickerToNews() { Url = i.article_url, ticker = h }).ToList()
                        }).ToHashSet();
            client.Dispose();
            Content.Dispose();
            return resp;
        }
        #endregion

        #region GetKandles
        [HttpGet]
        [Route("GetKandles")]
        public List<Polygon_StockKandles> GetKandles()
        {
            string url = "https://api.polygon.io/v2/snapshot/locale/us/markets/stocks/tickers?apiKey=1IDknqV7XjsFhZRNtwdNcJOtPp9IH0Ji";
            var resp_tickers = new List<Polygon_StockKandles>();
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var r = client.ExecuteAsync(request).Result.Content;

            var Content = new StringContent(r.ToString(), Encoding.UTF8, "application/json");
            JavaScriptSerializer? js = new JavaScriptSerializer();
            var poly_tickers = js.Deserialize<RootKandles>(r);
            foreach(var a in poly_tickers.tickers)
            {
                var rsp = new Polygon_StockKandles();
                rsp.symbol = a.ticker;
                rsp.source = "Polygon";
                rsp.openTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(a.updated) / 1000000000).UtcDateTime;
                rsp.closeTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(a.updated) / 1000000000).UtcDateTime;
                if (Convert.ToDecimal(a.day.c) != 0)
                {
                    rsp.openPrice = Convert.ToDecimal(a.day.o);
                    rsp.highPrice = Convert.ToDecimal(a.day.h);
                    rsp.lowPrice = Convert.ToDecimal(a.day.l);
                    rsp.closePrice = Convert.ToDecimal(a.day.c);
                    rsp.volume = Convert.ToDecimal(a.day.v);
                }
                else
                {
                    rsp.openPrice = Convert.ToDecimal(a.prevDay.o);
                    rsp.highPrice = Convert.ToDecimal(a.prevDay.h);
                    rsp.lowPrice = Convert.ToDecimal(a.prevDay.l);
                    rsp.closePrice = Convert.ToDecimal(a.prevDay.c);
                    rsp.volume = Convert.ToDecimal(a.prevDay.v);
                }
                rsp.quoteVolume = 0;
                rsp.tradeCount = 0;
                rsp.takerBuyBaseVolume = 0;
                rsp.takerBuyQuoteVolume = 0;
                resp_tickers.Add(rsp);
            }
            client.Dispose();
            Content.Dispose();
            return resp_tickers;
        }
        #endregion
        #region GetInstruments
        [HttpGet]
        [Route("GetInstruments")]
        public PolygonTickers GetInstruments(string? url_get = "https://api.polygon.io/v3/reference/tickers?active=true&limit=1")
        {
            var url = url_get + api;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var r = client.ExecuteAsync(request).Result.Content;

            var Content = new StringContent(r.ToString(), Encoding.UTF8, "application/json");
            JavaScriptSerializer? js = new JavaScriptSerializer();
            var poly_tickers = js.Deserialize<PolygonTickers>(r);
            Content.Dispose();
            client.Dispose();
            return poly_tickers;

        }
        #endregion

        #region GetInstrumentsDescription
        [HttpGet]
        [Route("GetInstrumentsDescription")]
        public api.allinoneapi.Models.Stocks.Polygon.Results GetInstrumentsDescription(string? ticker="AAPL")
        {
            string? url_get = "https://api.polygon.io/v3/reference/tickers/";
            var url = url_get + ticker + api2;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var r = client.ExecuteAsync(request).Result.Content;

            var Content = new StringContent(r.ToString(), Encoding.UTF8, "application/json");
            JavaScriptSerializer? js = new JavaScriptSerializer();
            var poly_tickers = js.Deserialize<api.allinoneapi.Models.Stocks.Polygon.TickerDescription>(r);
            client.Dispose();
            Content.Dispose();
            return poly_tickers.results;

        }
        #endregion

        #region GetPrice
        [HttpGet]
        [Route("GetPrice")]
        public string GetPrice(string symbol="SPY")
        {
            //var stock_prices = new api.allinoneapi.Models.Stocks.Polygon.Actions.Polygon();
            //return stock_prices.GetPrice(symbol);
            return new api.allinoneapi.Models.Stocks.Polygon.Actions.Polygon().GetPrice(symbol).results.First().c.ToString();
        }
        #endregion

        #region GetStocks
        [HttpGet]
        [Route("GetInstruments/Stocks")]
        public async Task<RepeatedField<Share>> GetStocks(CancellationToken stoppingToken)
        {
            return await _stock.GetStocks(stoppingToken, _investApi);
        }
        #endregion

        #region GetBonds
        [HttpGet]
        [Route("GetInstruments/Bonds")]
        public async Task<RepeatedField<Bond>> GetBonds(CancellationToken stoppingToken)
        {
            return await _stock.GetBonds(stoppingToken, _investApi);
        }
        #endregion

        #region GetFutures
        [HttpGet]
        [Route("GetInstruments/Futures")]
        public async Task<RepeatedField<Future>> GetFutures(CancellationToken stoppingToken)
        {
            return await _stock.GetFutures(stoppingToken, _investApi);
        }
        #endregion

        #region GetEtfs
        [HttpGet]
        [Route("GetInstruments/Etfs")]
        public async Task<RepeatedField<Etf>> GetEtfs(CancellationToken stoppingToken)
        {
            return await _stock.GetEtfs(stoppingToken, _investApi);
        }
        #endregion

        #region TradingScheduleSPB
        [HttpGet]
        [Route("GetSchedule/SPB")]
        public async Task<TradingSchedulesResponse> GetScheduleSPB(CancellationToken stoppingToken)
        {
            return await _stock.GetSchedule(stoppingToken, _investApi);
        }
        #endregion

        #region DisposeCtor
        ~StockController() { }
        public void Dispose()
        {

        }
        #endregion
    }
}
