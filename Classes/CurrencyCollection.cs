using Grandmark;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using Zephry;

namespace Grandmark
{
    public class CurrencyCollection : Zephob
    {
        #region Fields
        private List<Currency> _currencyList = new();
        #endregion

        #region  Properties

        [JsonProperty("list")]
        public List<Currency> CurrencyList { get => _currencyList; set => _currencyList = value; }
        #endregion

        #region Methods
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not CurrencyCollection)
            {
                throw new ArgumentException("aCurrencyCollection");
            }

            _currencyList.Clear();
            foreach (var vCurrencySource in ((CurrencyCollection)aSource)._currencyList)
            {
                var vCurrencyTarget = new Currency();
                vCurrencyTarget.AssignFromSource(vCurrencySource);
                _currencyList.Add(vCurrencyTarget);
            }
        }
        #endregion
    }
}
