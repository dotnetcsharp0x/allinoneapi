using api.allinoneapi.Models;
using Binance.Net.Clients;
using Tinkoff.InvestApi.V1;

namespace api.allinoneapi
{
    public class Crypto : IDisposable
    {
        #region Binance

        #region GetSymbols
        public HashSet<Crypto_Symbols> Binance_GetSymbols()
        {
            BinanceClient client = new();
            var resp1 = client.SpotApi.ExchangeData.GetProductsAsync().Result;
            Console.Write(resp1);
            var resp = client.SpotApi.ExchangeData.GetProductsAsync().Result.Data.Select(x => new Crypto_Symbols { Symbol = x.Symbol, QuoteAsset = x.QuoteAsset, BaseAsset = x.BaseAsset }).ToHashSet();
            client.Dispose();
            return resp;
        }
        #endregion

        #region GetCurrentPrices
        public HashSet<Crypto_Price> Binance_GetCurrentPrices()
        {
            BinanceClient client = new();
            var resp = client.SpotApi.ExchangeData.GetPricesAsync().Result.Data.Select(x => new Crypto_Price { Symbol = x.Symbol, Price = x.Price, DateTime = DateTime.Now }).ToHashSet();
            Console.WriteLine(resp);
            client.Dispose();
            return resp;
        }
        #endregion

        #region DayOfDayData
        public IEnumerable<Binance_CryptoKandles> Binance_DayOfDayData(string? symbol="BTCUSDT")
        {
            BinanceClient client = new();
            if (symbol != null)
            {
                var resp = client.SpotApi.ExchangeData.GetTickersAsync().Result.Data.Where(x => x.Symbol == symbol).Select(x => new Binance_CryptoKandles
                {
                    source = "Binance"
                    ,
                    symbol = x.Symbol,
                    openPrice = x.OpenPrice
                    ,
                    closePrice = x.PrevDayClosePrice
                    ,
                    volume = x.Volume
                    ,
                    lowPrice = x.LowPrice
                    ,
                    highPrice = x.HighPrice
                    ,
                    openTime = x.OpenTime
                    ,
                    closeTime = x.CloseTime
                    ,
                    tradeCount = x.TotalTrades
                    ,
                    quoteVolume = x.QuoteVolume
                });
                Console.WriteLine(resp);
                client.Dispose();
                return resp;
            }
            else
            {
                var resp = client.SpotApi.ExchangeData.GetTickersAsync().Result.Data.Select(x => new Binance_CryptoKandles
                {
                    source = "Binance"
                    ,
                    symbol = x.Symbol,
                    openPrice = x.OpenPrice
                    ,
                    closePrice = x.PrevDayClosePrice
                    ,
                    volume = x.Volume
                    ,
                    lowPrice = x.LowPrice
                    ,
                    highPrice = x.HighPrice
                    ,
                    openTime = x.OpenTime
                    ,
                    closeTime = x.CloseTime
                    ,
                    tradeCount = x.TotalTrades
                    ,
                    quoteVolume = x.QuoteVolume
                });
                Console.WriteLine(resp);
                client.Dispose();
                return resp;
            }

        }
        #endregion

        #region GetKandles
        public HashSet<Binance_CryptoKandles> Binance_GetKandles(string? symbol,int seconds,int lines, string interval)
        {
            if (symbol != null)
            {
                BinanceClient client = new();
                var inter= Binance.Net.Enums.KlineInterval.OneMinute;
                if (interval == "5M")
                {
                    inter = Binance.Net.Enums.KlineInterval.FiveMinutes;
                }
                if (interval == "1H")
                {
                    inter = Binance.Net.Enums.KlineInterval.OneHour;
                }
                if (interval == "1D")
                {
                    inter = Binance.Net.Enums.KlineInterval.OneDay;
                }
                if (interval == "1W")
                {
                    inter = Binance.Net.Enums.KlineInterval.OneWeek;
                }
                if (interval == "1M")
                {
                    inter = Binance.Net.Enums.KlineInterval.OneMonth;
                }
                if (interval == "1Y")
                {
                    inter = Binance.Net.Enums.KlineInterval.OneMonth;
                }
                if (interval == "5Y")
                {
                    inter = Binance.Net.Enums.KlineInterval.OneMonth;
                }
                var r = client.SpotApi.ExchangeData.GetKlinesAsync(symbol, inter, DateTime.Now.AddMinutes(seconds), DateTime.Now.AddMinutes(0), lines).Result.Data;
                if (r != null)
                {
                    return r.Select(x => new Binance_CryptoKandles { openTime = x.OpenTime, openPrice = x.OpenPrice, highPrice = x.HighPrice, lowPrice = x.LowPrice, closePrice = x.ClosePrice, volume = x.Volume, closeTime = x.CloseTime, quoteVolume = x.QuoteVolume, tradeCount = x.TradeCount, takerBuyBaseVolume = x.TakerBuyBaseVolume, takerBuyQuoteVolume = x.TakerBuyQuoteVolume, symbol = symbol, source = "Binance" }).ToHashSet();

                }
                else
                {
                    return new HashSet<Binance_CryptoKandles>();
                }
            }
            else
            {
                return new HashSet<Binance_CryptoKandles>();
            }
        }
        #endregion

        #endregion

        #region DisposeCtor
        public void Dispose()
        {
            
        }

        ~Crypto() { }
        #endregion
    }
}