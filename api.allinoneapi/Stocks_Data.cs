using Google.Protobuf.Collections;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace api.allinoneapi
{
    public class Stocks_Data 
    {
        private readonly InvestApiClient _investApi;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly ILogger<Stocks_Data> _logger;
        public Stocks_Data(ILogger<Stocks_Data> logger, InvestApiClient investApi, IHostApplicationLifetime lifetime)
        {
            _logger = logger;
            _investApi = investApi;
            _lifetime = lifetime;
        }

        #region Tinkoff

        #region GetStocks
        public async Task<RepeatedField<Share>> GetStocks(CancellationToken stoppingToken, InvestApiClient _investApi)
        {
            var instrumentsDescription = await new InstrumentsServiceSample(_investApi.Instruments)
           .GetStocks(stoppingToken);
            return instrumentsDescription;
        }
        #endregion

        #region GetBonds
        public async Task<RepeatedField<Bond>> GetBonds(CancellationToken stoppingToken, InvestApiClient _investApi)
        {
            var instrumentsDescription = await new InstrumentsServiceSample(_investApi.Instruments)
           .GetBonds(stoppingToken);
            return instrumentsDescription;
        }
        #endregion

        #region GetFutures
        public async Task<RepeatedField<Future>> GetFutures(CancellationToken stoppingToken, InvestApiClient _investApi)
        {
            var instrumentsDescription = await new InstrumentsServiceSample(_investApi.Instruments)
           .GetFutures(stoppingToken);
            return instrumentsDescription;
        }
        #endregion

        #region GetEtfs
        public async Task<RepeatedField<Etf>> GetEtfs(CancellationToken stoppingToken, InvestApiClient _investApi)
        {
            var instrumentsDescription = await new InstrumentsServiceSample(_investApi.Instruments)
           .GetEtfs(stoppingToken);
            return instrumentsDescription;
        }
        #endregion

        #region GetSchedule
        public async Task<TradingSchedulesResponse> GetSchedule(CancellationToken stoppingToken, InvestApiClient _investApi)
        {
            var instrumentsDescription = await new InstrumentsServiceSample(_investApi.Instruments)
           .TradingSchedulesResponseSPB(stoppingToken);
            return instrumentsDescription;
        }
        #endregion

        #endregion
    }
}
