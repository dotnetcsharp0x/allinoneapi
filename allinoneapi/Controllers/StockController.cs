using api.allinoneapi;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nancy.Bootstrapper;
using System.Text;
using System.Threading;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;
using static Tinkoff.InvestApi.V1.InstrumentsService;

namespace allinoneapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase, IDisposable
    {
        InvestApiClient _investApi;
        Stocks_Data stock = new Stocks_Data();
        public StockController(InvestApiClient investApi)
        {
            _investApi = investApi; 
        }
        public void Dispose()
        {
            
        }
        #region Test
        [HttpGet]
        [Route("GetInstruments")]
        public async Task<SharesResponse> GetInstruments(CancellationToken stoppingToken)
        {
            return await stock.GetInstruments(stoppingToken, _investApi);
        }
        #endregion
    }
}
