using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    public class ExchangeRateCollection :Zephob
    {
        #region Fields
        private List<ExchangeRate> _exchangeRateList = new();
        #endregion

        #region  Properties

        [JsonProperty("list")]
        public List<ExchangeRate> ExchangeRateList { get => _exchangeRateList; set => _exchangeRateList = value; }
        #endregion

        #region Methods
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not ExchangeRateCollection)
            {
                throw new ArgumentException("aExchangeRateCollection");
            }

            _exchangeRateList.Clear();
            foreach (var vExchangeRateSource in ((ExchangeRateCollection)aSource)._exchangeRateList)
            {
                var vExchangeRateTarget = new ExchangeRate();
                vExchangeRateTarget.AssignFromSource(vExchangeRateSource);
                _exchangeRateList.Add(vExchangeRateTarget);
            }
        }
        #endregion
    }
}
