using Microsoft.AspNetCore.Mvc;
using Binance.Net.Clients;
using System.Collections;
using BenchmarkDotNet.Attributes;
using allinoneapi.Data;
using RestSharp;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Binance.Net.Objects.Models.Spot;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using api.allinoneapi;
using api.allinoneapi.Models;
using Binance.Net.Enums;

namespace allinoneapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase, IDisposable
    {
        Crypto crypto = new Crypto();
        public CryptoController() { }

        #region UpdatePairs
        [HttpGet]
        [Route("UpdatePairs")]
        public HashSet<Crypto_Symbols> UpdatePairs()
        {
            return crypto.Binance_GetSymbols();
        }
        #endregion

        #region UpdateCurrentPrice
        [HttpGet]
        [Route("UpdateCurrentPrice")]
        public HashSet<Crypto_Price> UpdateCurrentPrice()
        {
            return crypto.Binance_GetCurrentPrices();
        }
        #endregion

        #region DayOfDayData
        [HttpGet]
        [Route("DayOfDayData")]
        public IEnumerable<Binance_CryptoKandles> DayOfDayData(string? symbol)
        {
            return crypto.Binance_DayOfDayData(symbol);
        }
        #endregion

        #region GetKandles
        [HttpGet]
        [Route("GetKandles")]
        public Binance_CryptoKandles GetKandles(string? symbol,int lines)
        {
            return crypto.Binance_GetKandles(symbol,-182, lines);
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