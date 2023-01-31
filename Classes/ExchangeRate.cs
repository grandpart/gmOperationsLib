using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    public class ExchangeRate : Zephob
    {
        #region Fields
        private int _entkey;
        private int _exrkey;
        private int _curkey;
        private int _exrfinyear;
        private int _exrfinmonth;
        private decimal _exrrate;
        #endregion

        #region Properties
        [JsonProperty("entkey")]
        public int EntKey { get { return _entkey; } set { _entkey = value; } }

        [JsonProperty("exrkey")]
        public int ExrKey { get { return _exrkey; } set { _exrkey = value; } }

        [JsonProperty("curkey")]
        public int CurKey { get { return _curkey; } set { _curkey = value; } }

        [JsonProperty("exrfinyear")]
        public int ExrFinYear { get { return _exrfinyear; } set { _exrfinyear = value; } }

        [JsonProperty("exrfinmonth")]
        public int ExrFinMonth { get { return _exrfinmonth; } set { _exrfinmonth = value; } }

        [JsonProperty("exrrate")]
        public decimal ExrRate { get { return _exrrate; } set { _exrrate = value; } }
        #endregion

        #region Constructor
        public ExchangeRate() { }

        public ExchangeRate(int aEntKey, int aExrKey)
        {
            _entkey = aEntKey;
            _exrkey = aExrKey;
        }
        #endregion

        #region Comparer
        public class EqualityComparer : IEqualityComparer<ExchangeRate>
        {
            public bool Equals(ExchangeRate aExchangeRate1, ExchangeRate aExchangeRate2)
            {
                return aExchangeRate1._entkey == aExchangeRate2._entkey &&
                    aExchangeRate1._exrkey == aExchangeRate2._exrkey;
            }

            public int GetHashCode(ExchangeRate aExchangeRate)
            {
                return Convert.ToInt32(aExchangeRate._entkey) ^ Convert.ToInt32(aExchangeRate._exrkey);
            }
        }
        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not ExchangeRate)
            {
                throw new ArgumentException("Invalid Source Argument to ExchangeRate Assign");
            }
            _entkey = ((ExchangeRate)aSource)._entkey;
            _exrkey = ((ExchangeRate)aSource)._exrkey;
            _curkey = ((ExchangeRate)aSource)._curkey;
            _exrfinyear = ((ExchangeRate)aSource)._exrfinyear;
            _exrfinmonth = ((ExchangeRate)aSource)._exrfinmonth;
            _exrrate = ((ExchangeRate)aSource)._exrrate;
        }
        #endregion
    }
}
