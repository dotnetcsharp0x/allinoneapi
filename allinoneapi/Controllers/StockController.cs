using api.allinoneapi;
using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Mvc;
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
        Stocks_Data _stock;
        public StockController(ILogger<Stocks_Data> logger, InvestApiClient investApi, IHostApplicationLifetime lifetime)
        {
            _logger = logger;
            _lifetime = lifetime;
            _investApi = investApi;
            _stock = new Stocks_Data(_logger, _investApi, _lifetime);
        }

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
