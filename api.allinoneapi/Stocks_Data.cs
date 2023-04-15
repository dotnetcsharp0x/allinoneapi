using api.allinoneapi;
using CryptoExchange.Net.Interfaces;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace api.allinoneapi
{
    
    public class Stocks_Data
    {
        public async Task<SharesResponse> GetInstruments(CancellationToken stoppingToken, InvestApiClient _investApi)
        {
            var instrumentsDescription = await new InstrumentsServiceSample(_investApi.Instruments).GetInstrumentByTicker(stoppingToken);
            return instrumentsDescription;
        }
    }
}
