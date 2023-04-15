using Microsoft.AspNetCore.Mvc;
using api.allinoneapi;
using api.allinoneapi.Models;
using System.Collections.Generic;
using AspNetCoreRateLimit;

namespace allinoneapi.Controllers
{

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
        public HashSet<Crypto_Symbols> GetInstruments()
        {
            return crypto.Binance_GetSymbols();
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
        public HashSet<Binance_CryptoKandles> GetKandles(string? symbol = "BTCUSDT",int minutes=1,int lines=1)
        {
            try
            {
                int to_minus = -182;
                to_minus -= minutes;
                var resp = crypto.Binance_GetKandles(symbol, to_minus, lines);
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