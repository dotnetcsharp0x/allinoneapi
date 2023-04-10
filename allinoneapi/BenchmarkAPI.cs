using api.allinoneapi;
using BenchmarkDotNet.Attributes;

//using HtmlExporterAttribute = BenchmarkDotNet.Attributes.Exporters.HtmlExporterAttribute;

namespace allinoneapi
{
    [HtmlExporter]
    public class BenchmarkAPI
    {
        //[Benchmark]
        //public IImmutableList<Crypto_Price> UpdateCurrentPrice()
        //{
        //    BinanceClient client = new();
        //    var resp = client.SpotApi.ExchangeData.GetPricesAsync().Result.Data.Select(x => new Crypto_Price { Symbol = x.Symbol, Price = x.Price, DateTime = DateTime.Now }).ToImmutableArray();
        //    client.Dispose();
        //    //Thread.Sleep(4000);
        //    return resp;
        //}
        [Benchmark]
        public void Test1()
        {
            Crypto crypto = new Crypto();
            crypto.Binance_GetCurrentPrices();
            crypto.Dispose();
            //Thread.Sleep(3000);
        }
        //[Benchmark]
        //public IEnumerable<Crypto_Price> UpdateCurrentPrice3()
        //{
        //    BinanceClient client = new();
        //    var resp = client.SpotApi.ExchangeData.GetPricesAsync().Result.Data.Select(x => new Crypto_Price { Symbol = x.Symbol, Price = x.Price, DateTime = DateTime.Now }).ToList();
        //    client.Dispose();
        //    Thread.Sleep(4000);
        //    return resp;
        //}
    }
}
