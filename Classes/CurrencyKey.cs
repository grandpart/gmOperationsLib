using Grandmark;
using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    public class CurrencyKey : Zephob
    {
        #region Fields
        private int _entkey;
        private int _curkey;
        #endregion

        #region Properties
        /// <summary>
        /// The [JsonProperty fields will also be the fields listed on your body when submitting the request]
        /// </summary>

        [JsonProperty("entkey")]
        public int EntKey { get => _entkey; set { _entkey = value; } }

        [JsonProperty("curkey")]
        public int CurKey { get => _curkey; set { _curkey = value; } }
        #endregion

        #region Constructors
        public CurrencyKey() { }

        public CurrencyKey(int aEntKey, int aCurKey)
        {
            _entkey = aEntKey;
            _curkey = aCurKey;
        }
        #endregion

        #region Comparer
        public class EqualityComparer : IEqualityComparer<CurrencyKey>
        {
            public bool Equals(CurrencyKey aCurrencyKey1, CurrencyKey aCurrencyKey2)
            {
                return aCurrencyKey1._entkey == aCurrencyKey2._entkey &&
                    aCurrencyKey1._curkey == aCurrencyKey2._curkey;
            }

            public int GetHashCode(CurrencyKey aCurrencyKey)
            {
                return Convert.ToInt32(aCurrencyKey._entkey) ^ Convert.ToInt32(aCurrencyKey._curkey);
            }
        }
        #endregion

        #region AssignmentFromSource
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is CurrencyKey))
            {
                throw new ArgumentException("Invalid assignment source", "CurrencyKey");
            }
            _entkey = ((CurrencyKey)aSource)._entkey;
            _curkey = ((CurrencyKey)aSource)._curkey;
        }
        #endregion
    }
}
