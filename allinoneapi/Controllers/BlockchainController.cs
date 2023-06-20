using api.allinoneapi;
using api.allinoneapi.Models;
using api.allinoneapi.Models.Blockchain.Bitcoin;
using AspNetCoreRateLimit;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System.Text;
using System.Xml;

namespace allinoneapi.Controllers
{
    [Route("api/Blockchain")]
    [ApiController]
    public class BlockchainController : ControllerBase, IDisposable
    {
        Crypto crypto = new Crypto();
        private readonly ILogger<BlockchainController> _logger;
        private readonly IIpPolicyStore _policyStore;
        string api = "";
        public BlockchainController(ILogger<BlockchainController> logger, IIpPolicyStore policyStore)
        {
            XmlDocument xApi = new XmlDocument();
            xApi.Load("api.xml");
            XmlElement? xRootApi = xApi.DocumentElement;
            if (xRootApi != null)
            {
                foreach (XmlElement xnode in xRootApi)
                {
                    api = xnode.ChildNodes[2].InnerText;
                }
            }
            _logger = logger;
            _policyStore = policyStore;
        }

        #region Bitcoin/Stats/Blocks/Count
        [HttpGet]
        [Route("Bitcoin/Stats/Blocks/Count")]
        public List<api.allinoneapi.Models.Blockchain.Bitcoin.Bitcoin> BitcoinBlocksCount(string? date)
        {
            var url = "https://graphql.bitquery.io";
            var client = new RestClient("https://graphql.bitquery.io");
            client.Options.Timeout = -1;
            var request = new RestRequest(url, RestSharp.Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-API-KEY", api);
            var body = "";
            if (date==null)
            {
                body = @"{""query"":""{\n  bitcoin {\n    blocks {\n      count\n    }\n  }\n}\n"",""variables"":""{}""}";
            }
            else
            {
                body = @"{""query"":""{\n  bitcoin {\n    blocks(date: {after: \"""+date;
                body = body + @"\""}) {\n      count\n      date {\n        date\n      }\n    }\n  }\n}\n"",""variables"":""{}""}";
            }
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            var Content = new StringContent(response.Content.ToString(), Encoding.UTF8, "application/json");
            JavaScriptSerializer? js = new JavaScriptSerializer();
            var r = response.Content;
            var gecko_symbols = JsonConvert.DeserializeObject<Root>(r);
            var resp = new List<api.allinoneapi.Models.Blockchain.Bitcoin.Bitcoin>();
            foreach(var a in gecko_symbols.Data.bitcoin.blocks)
            {
                var item = new api.allinoneapi.Models.Blockchain.Bitcoin.Bitcoin();
                item.Value = a.count;
                if (date == null)
                {
                    item.Date = DateTime.Now;
                }
                else
                {
                    item.Date = Convert.ToDateTime(a.date.date);
                }
                resp.Add(item);
            }
            Content.Dispose();
            client.Dispose();
            return resp;
        }
        #endregion

        #region Bitcoin/Stats/Difficulty
        [HttpGet]
        [Route("Bitcoin/Stats/Difficulty")]
        public List<api.allinoneapi.Models.Blockchain.Bitcoin.Bitcoin> Difficulty(string? date)
        {
            var url = "https://graphql.bitquery.io";
            var client = new RestClient("https://graphql.bitquery.io");
            client.Options.Timeout = -1;
            var request = new RestRequest(url, RestSharp.Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-API-KEY", api);
            if (date == null)
            {
                date = DateTime.Now.AddDays(-1).ToString("yyy-MM-dd");
            }
            var body = @"{""query"":""query($after: ISO8601DateTime!) {\n  bitcoin {\n    blocks(date: {after: $after}, difficulty: {}) {\n      date {\n        date\n      }\n      difficulty\n    }\n  }\n}\n""
            ,""variables"":""{\""after\"":\"""+date;
            body=body+@"\""}""}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            var Content = new StringContent(response.Content.ToString(), Encoding.UTF8, "application/json");
            JavaScriptSerializer? js = new JavaScriptSerializer();
            var r = response.Content;
            var gecko_symbols = JsonConvert.DeserializeObject<Root>(r);
            List<api.allinoneapi.Models.Blockchain.Bitcoin.Bitcoin> difficulty = new List<api.allinoneapi.Models.Blockchain.Bitcoin.Bitcoin>();
            foreach (var a in gecko_symbols.Data.bitcoin.blocks)
            {
                api.allinoneapi.Models.Blockchain.Bitcoin.Bitcoin ina = new api.allinoneapi.Models.Blockchain.Bitcoin.Bitcoin();
                ina.Date = Convert.ToDateTime(a.date.date);
                ina.Value = a.difficulty;
                difficulty.Add(ina);
            }
            client.Dispose();
            Content.Dispose();
            return difficulty;
        }
        #endregion

        #region GetBalance
        [HttpGet]
        [Route("GetBalance")]
        public Address GetBalance(string address= "1NYCiNjK1TGmC8sJyNfbRCVJuTHtDx88jZ")
        {
            var adr = new Address();
            var url = "https://graphql.bitquery.io";
            var client = new RestClient("https://graphql.bitquery.io");
            client.Options.Timeout = -1;
            var request = new RestRequest(url, RestSharp.Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-API-KEY", api);

            var body = @"{""query"":""query {\n  bitcoin(network: bitcoin) {\n    addressStats(address: {in: \""";
            body = body + address;
            body = body + @"\""}) {\n      address {\n        balance_usd: balance(in: USD)\n        address\n      }\n    }\n  }\n}"",""variables"":""{}""}";

            //var body = @"{""query"":""query {\n  bitcoin(network: bitcoin) {\n    addressStats(address: {in: \""34xp4vRoCGJym3xR7yCVPFHoCNxv4Twseo\""}) {\n      address {\n        balance_usd: balance(in: USD)\n        address\n      }\n    }\n  }\n}"",""variables"":""{}""}";

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            var Content = new StringContent(response.Content.ToString(), Encoding.UTF8, "application/json");
            JavaScriptSerializer? js = new JavaScriptSerializer();
            var r = response.Content;
            var gecko_symbols = JsonConvert.DeserializeObject<Root>(r);
            double resp = 0;
            if (gecko_symbols.Data.bitcoin.addressStats.Count > 0)
            {
                adr = gecko_symbols.Data.bitcoin.addressStats.First().address;
            }
            adr.address = address;
            client.Dispose();
            Content.Dispose();
            return adr;
        }
        #endregion



        #region DisposeCtor
        ~BlockchainController()
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
