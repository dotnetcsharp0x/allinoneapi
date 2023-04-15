using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace api.allinoneapi
{
    
    public class Stocks_Data
    {
        #region GetInstruments
        public async Task<SharesResponse> GetInstruments(CancellationToken stoppingToken, InvestApiClient _investApi)
        {
            var instrumentsDescription = await new InstrumentsServiceSample(_investApi.Instruments).GetInstrumentByTicker(stoppingToken);
            return instrumentsDescription;
        }
        #endregion
    }
}
