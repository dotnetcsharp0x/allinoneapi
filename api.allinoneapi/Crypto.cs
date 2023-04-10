﻿using api.allinoneapi.Models;
using Binance.Net.Clients;

namespace api.allinoneapi
{
    public class Crypto : IDisposable
    {
        #region Binance

        #region GetSymbols
        public HashSet<Crypto_Symbols> Binance_GetSymbols()
        {
            BinanceClient client = new();
            Console.WriteLine("log");
            var resp = client.SpotApi.ExchangeData.GetProductsAsync().Result.Data.Select(x => new Crypto_Symbols { Symbol = x.Symbol, QuoteAsset = x.QuoteAsset, BaseAsset = x.BaseAsset }).ToHashSet();
            client.Dispose();
            return resp;
        }
        #endregion

        #region GetCurrentPrice
        public HashSet<Crypto_Price> Binance_GetCurrentPrice()
        {
            BinanceClient client = new();
            var resp = client.SpotApi.ExchangeData.GetPricesAsync().Result.Data.Select(x => new Crypto_Price { Symbol = x.Symbol, Price = x.Price, DateTime = DateTime.Now }).ToHashSet();
            Console.WriteLine(resp);
            client.Dispose();
            return resp;
        }
        #endregion

        #region DayOfDayData
        public IEnumerable<Binance_CryptoKandles> Binance_DayOfDayData(string? symbol)
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
        public Binance_CryptoKandles Binance_GetKandles(string symbol,int seconds,int lines)
        {
            if (symbol.Length > 0)
            {
                BinanceClient client = new();
                var r = client.SpotApi.ExchangeData.GetKlinesAsync(symbol, Binance.Net.Enums.KlineInterval.OneMinute, DateTime.Now.AddMinutes(seconds), DateTime.Now.AddMinutes(0), lines).Result.Data;
                return r.Select(x => new Binance_CryptoKandles { openTime = x.OpenTime, openPrice = x.OpenPrice, highPrice = x.HighPrice, lowPrice = x.LowPrice, closePrice = x.ClosePrice, volume = x.Volume, closeTime = x.CloseTime, quoteVolume = x.QuoteVolume, tradeCount = x.TradeCount, takerBuyBaseVolume = x.TakerBuyBaseVolume, takerBuyQuoteVolume = x.TakerBuyQuoteVolume, symbol = symbol, source = "Binance" }).First();
            }
            else
            {
                return new Binance_CryptoKandles();
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