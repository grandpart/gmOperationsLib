using Grandmark;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using Zephry;

namespace Grandmark
{
    public class CurrencyCollection : Zephob
    {
        #region Fields
        private List<Currency> _list = new();
        #endregion

        #region  Properties

        public List<Currency> List { get => _list; set => _list = value; }
        #endregion

        #region Methods
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not CurrencyCollection)
            {
                throw new ArgumentException("aCurrencyCollection");
            }

            _list.Clear();
            foreach (var vCurrencySource in ((CurrencyCollection)aSource)._list)
            {
                var vCurrencyTarget = new Currency();
                vCurrencyTarget.AssignFromSource(vCurrencySource);
                _list.Add(vCurrencyTarget);
            }
        }
        #endregion
    }
}
