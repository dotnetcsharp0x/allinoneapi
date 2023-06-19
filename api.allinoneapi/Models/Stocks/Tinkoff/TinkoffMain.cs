using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.allinoneapi.Models.Stocks.Tinkoff
{
    public class TinkoffMain
    {
        public string figi { get; set; }
        public string ticker { get; set; }
        public string classCode { get; set; }
        public string isin { get; set; }
        public int lot { get; set; }
        public string currency { get; set; }
        public Klong klong { get; set; }
        public Kshort kshort { get; set; }
        public Dlong dlong { get; set; }
        public Dshort dshort { get; set; }
        public DlongMin dlongMin { get; set; }
        public DshortMin dshortMin { get; set; }
        public bool shortEnabledFlag { get; set; }
        public string name { get; set; }
        public string exchange { get; set; }
        public int couponQuantityPerYear { get; set; }
        public MaturityDate maturityDate { get; set; }
        public Nominal nominal { get; set; }
        public InitialNominal initialNominal { get; set; }
        public StateRegDate stateRegDate { get; set; }
        public PlacementDate placementDate { get; set; }
        public PlacementPrice placementPrice { get; set; }
        public AciValue aciValue { get; set; }
        public string countryOfRisk { get; set; }
        public string countryOfRiskName { get; set; }
        public string sector { get; set; }
        public string issueKind { get; set; }
        public long issueSize { get; set; }
        public long issueSizePlan { get; set; }
        public int tradingStatus { get; set; }
        public bool otcFlag { get; set; }
        public bool buyAvailableFlag { get; set; }
        public bool sellAvailableFlag { get; set; }
        public bool floatingCouponFlag { get; set; }
        public bool perpetualFlag { get; set; }
        public bool amortizationFlag { get; set; }
        public MinPriceIncrement minPriceIncrement { get; set; }
        public bool apiTradeAvailableFlag { get; set; }
        public string uid { get; set; }
        public int realExchange { get; set; }
        public string positionUid { get; set; }
        public bool forIisFlag { get; set; }
        public bool forQualInvestorFlag { get; set; }
        public bool weekendFlag { get; set; }
        public bool blockedTcaFlag { get; set; }
        public bool subordinatedFlag { get; set; }
        public First1MinCandleDate first1MinCandleDate { get; set; }
        public First1DayCandleDate first1DayCandleDate { get; set; }
        public int riskLevel { get; set; }
    }
}
