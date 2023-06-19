using api.allinoneapi.Models;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using IBApi;
using Nancy;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace allinoneapi.Data
{
    public class Query 
    {
        private readonly IGraphQLClient _client;

        public List<Crypto_Symbols> getInstruments()
        {
            var query = new List<Crypto_Symbols>();
            using (allinoneapiContext _context = new allinoneapiContext())
            {
                query = (from i in _context.Crypto_Symbols select new Crypto_Symbols { 
                    Symbol = i.Symbol,
                    BaseAsset=i.BaseAsset,
                    QuoteAsset=i.QuoteAsset
                }).ToList();
            }
            return query;
        }

        public int test()
        {
            var url = "https://graphql.bitquery.io";
            var client = new RestClient("https://graphql.bitquery.io");
            client.Options.Timeout = -1;
            var request = new RestRequest(url,Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-API-KEY", "BQYmTYyjGDfFSDFkZ93bj3Zt5EhjeGFu");
            var body = @"{""query"":""{\n  bitcoin {\n    blocks {\n      count\n    }\n  }\n}\n"",""variables"":""{}""}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            var Content = new StringContent(response.Content.ToString(), Encoding.UTF8, "application/json");
            JavaScriptSerializer? js = new JavaScriptSerializer();
            var r = response.Content;
            var gecko_symbols = JsonConvert.DeserializeObject<Root>(r);
            var resp = 1;
            if (gecko_symbols != null)
            {
                resp = gecko_symbols.Data.bitcoin.blocks.First().count;
            }
            return resp;
        }

        public double balance()
        {
            var url = "https://graphql.bitquery.io";
            var client = new RestClient("https://graphql.bitquery.io");
            client.Options.Timeout = -1;
            var request = new RestRequest(url,Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-API-KEY", "BQYmTYyjGDfFSDFkZ93bj3Zt5EhjeGFu");
            var body = @"{""query"":""query {\n  bitcoin(network: bitcoin) {\n    addressStats(address: {in: \""34xp4vRoCGJym3xR7yCVPFHoCNxv4Twseo\""}) {\n      address {\n        balance_usd: balance(in: USD)\n        address\n      }\n    }\n  }\n}"",""variables"":""{}""}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            var Content = new StringContent(response.Content.ToString(), Encoding.UTF8, "application/json");
            JavaScriptSerializer? js = new JavaScriptSerializer();
            var r = response.Content;
            var gecko_symbols = JsonConvert.DeserializeObject<Root>(r);
            double resp = 0;
            if (gecko_symbols != null)
            {
                resp = gecko_symbols.Data.bitcoin.addressStats.First().address.balance_usd;
            }
            return resp;
        }
    }
}
