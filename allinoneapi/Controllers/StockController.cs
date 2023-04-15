using api.allinoneapi;
using Microsoft.AspNetCore.Mvc;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

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
        
        #region GetInstruments
        [HttpGet]
        [Route("GetInstruments")]
        public async Task<SharesResponse> GetInstruments(CancellationToken stoppingToken)
        {
            return await stock.GetInstruments(stoppingToken, _investApi);
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
