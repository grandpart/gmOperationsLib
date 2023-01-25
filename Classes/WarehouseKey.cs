using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    public class WarehouseKey : Zephob
    {
        #region Fields
        private int _entkey;
        private int _whskey;
        #endregion

        #region Properties
        /// <summary>
        /// The [JsonProperty fields will also be the fields listed on your body when submitting the request]
        /// </summary>
        /// 
        [JsonProperty("entkey")]
        public int EntKey { get => _entkey; set { _entkey = value; } }

        [JsonProperty("whskey")]
        public int WhsKey { get => _whskey; set { _whskey = value; } }
        #endregion

        #region Constructors
        public WarehouseKey() { }

        public WarehouseKey(int aEntKey, int aWhsKey)
        {
            _entkey = aEntKey;
            _whskey = aWhsKey;
        }
        #endregion

        #region Comparer
        public class EqualityComparer : IEqualityComparer<WarehouseKey>
        {
            public bool Equals(WarehouseKey aWarehouseKey1, WarehouseKey aWarehouseKey2)
            {
                return aWarehouseKey1._entkey == aWarehouseKey2._entkey &&
                    aWarehouseKey1._whskey == aWarehouseKey2._whskey;
            }

            public int GetHashCode(WarehouseKey aWarehouseKey)
            {
                return Convert.ToInt32(aWarehouseKey._entkey) ^ Convert.ToInt32(aWarehouseKey._whskey);
            }
        }
        #endregion

        #region AssignmentFromSource
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is WarehouseKey))
            {
                throw new ArgumentException("Invalid assignment source", "WarehouseKey");
            }
            _entkey = ((WarehouseKey)aSource)._entkey;
            _whskey = ((WarehouseKey)aSource)._whskey;
        }
        #endregion
    }
}
